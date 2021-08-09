using System;
using Script.GameBoard;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    public class GameController : MonoBehaviour
    {

        public BoardController boardControllerPrefab;

        public event StartTurnDelegate StartTurn;
        public event EndTurnDelegate EndTurn;
        
        private BoardController _boardController;
        private GameState _gameState;

        private void Start()
        {
            _gameState = GameState.PRETurnPhase;
        }

        private void GameLoop()
        {
            while (_gameState != GameState.ExitGame)
            {
                switch (_gameState)
                {
                    case GameState.PRETurnPhase:
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