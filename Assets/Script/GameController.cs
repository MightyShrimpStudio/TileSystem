using System;
using System.Collections.Generic;
using Script.GameBoard;
using Script.Systems;
using UnityEngine;
using CharacterController = Script.Character.CharacterController;

namespace Script
{
    public class GameController : MonoBehaviour
    {

        public BoardController boardControllerPrefab;
        public List<CharacterController> characters;
        
        public event StartTurnDelegate StartTurn;
        public event EndTurnDelegate EndTurn;
        
        private BoardController _boardController;
        private GameState _currentGameState;
        private GameState _prevGameState;
        private readonly CharacterOrder _characterOrder = new CharacterOrder();
        private bool _newPhase;

        private void Start()
        {
            SetUpGame();
        }

        private void SetUpGame()
        {
            _boardController = Instantiate(boardControllerPrefab,transform.position,Quaternion.identity);
            _characterOrder.InGameCharacters = characters;
            _characterOrder.CalculateOrder();
            _currentGameState = GameState.PRETurnPhase;
            _newPhase = true;
        }

        private void Update()
        {
            if (_newPhase)
            {
                _newPhase = false;
                switch (_currentGameState)
                {
                    case GameState.PRETurnPhase:
                        _characterOrder.NextCharacter();
                        NextPhase();
                        break;
                    case GameState.StartTurnPhase:
                        OnStartTurn();
                        NextPhase();
                        break;
                    case GameState.ActionPhase:
                        break;
                    case GameState.EndTurnPhase:
                        OnEndTurn();
                        NextPhase();
                        break;
                    case GameState.ExitGame:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void TriggerEndTurn()
        {
            _currentGameState = GameState.EndTurnPhase;
        }

        private void NextPhase()
        {
            switch (_currentGameState)
            {
                case GameState.PRETurnPhase:
                    _currentGameState = GameState.StartTurnPhase;
                    break;
                case GameState.StartTurnPhase:
                    _currentGameState = GameState.ActionPhase;
                    break;
                case GameState.ActionPhase:
                    _currentGameState = GameState.EndTurnPhase;
                    break;
                case GameState.EndTurnPhase:
                    _currentGameState = GameState.PRETurnPhase;
                    break;
                case GameState.ExitGame:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _newPhase = true;
        }
        
        private void OnStartTurn()
        {
            StartTurn?.Invoke();
        }

        private void OnEndTurn()
        {
            EndTurn?.Invoke();
        }
    }

    public delegate void StartTurnDelegate();
    public delegate void EndTurnDelegate();

    public enum GameState
    {
        PRETurnPhase, StartTurnPhase, ActionPhase, EndTurnPhase,
        ExitGame
    }
}