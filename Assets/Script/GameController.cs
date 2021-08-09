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
        private GameState _gameState;
        private CharacterController _currentCharacter;
        private CharacterOrder _characterOrder = new CharacterOrder();

        private void Start()
        {
            SetUpGame();
            //GameLoop();
        }

        private void SetUpGame()
        {
            _boardController = Instantiate(boardControllerPrefab,transform.position,Quaternion.identity);
            _characterOrder.InGameCharacters = characters;
            _characterOrder.CalculateOrder();
            _gameState = GameState.PRETurnPhase;
        }

        private void GameLoop()
        {
            while (_gameState != GameState.ExitGame)
            {
                switch (_gameState)
                {
                    case GameState.PRETurnPhase:
                        _characterOrder.NextCharacter();
                        _gameState = GameState.StartTurnPhase;
                        break;
                    case GameState.StartTurnPhase:
                        OnStartTurn();
                        _gameState = GameState.ActionPhase;
                        break;
                    case GameState.ActionPhase:
                        _gameState = GameState.EndTurnPhase;
                        break;
                    case GameState.EndTurnPhase:
                        OnEndTurn();
                        _gameState = GameState.PRETurnPhase;
                        break;
                    case GameState.ExitGame:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
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