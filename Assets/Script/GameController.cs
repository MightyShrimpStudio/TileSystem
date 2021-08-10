using System;
using System.Collections.Generic;
using Script.Entity.Character;
using Script.GameBoard;
using Script.SubSystems;
using UnityEngine;

namespace Script
{
    public class GameController : MonoBehaviour
    {
        public BoardController boardControllerPrefab;
        public List<CreatureController> characters;
        private readonly CharacterOrder _characterOrder = new();

        private BoardController _boardController;
        private GameStateMachine _gameStateMachine;

        private void Awake()
        {
            _gameStateMachine = new GameStateMachine();
        }

        private void Start()
        {
            SetUpGame();
        }

        private void Update()
        {
            if (_gameStateMachine.NewPhase)
            {
                _gameStateMachine.NewPhase = false;
                switch (_gameStateMachine.CurrentGameState)
                {
                    case GameStateMachine.GameState.PRETurnPhase:
                        _characterOrder.NextCharacter();
                        _gameStateMachine.NextPhase();
                        break;
                    case GameStateMachine.GameState.StartTurnPhase:
                        OnStartTurn();
                        _gameStateMachine.NextPhase();
                        break;
                    case GameStateMachine.GameState.ActionPhase:
                        break;
                    case GameStateMachine.GameState.EndTurnPhase:
                        OnEndTurn();
                        _gameStateMachine.NextPhase();
                        break;
                    case GameStateMachine.GameState.ExitGame:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public event StartTurnDelegate StartTurn;
        public event EndTurnDelegate EndTurn;

        private void SetUpGame()
        {
            _boardController = Instantiate(boardControllerPrefab, transform.position, Quaternion.identity);
            _boardController.Populate();
            _characterOrder.InGameCharacters = characters;
            _characterOrder.CalculateOrder();
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
}