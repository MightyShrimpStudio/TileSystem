using System;
using Script.Entity.Character;
using Script.GameBoard;
using Script.GameBoard.Tile;
using Script.SubSystems;
using UnityEngine;

namespace Script
{
    [RequireComponent(typeof(CharacterOrder))]
    public class GameController : MonoBehaviour
    {
        public BoardController boardControllerPrefab;

        private CharacterOrder _characterOrder;
        private BoardController _boardController;
        private GameStateMachine _gameStateMachine;
        private PathFinder _pathFinder;

        private void Awake()
        {
            _characterOrder = GetComponent<CharacterOrder>();
            _gameStateMachine = new GameStateMachine();
            _pathFinder = new PathFinder();
        }

        private void Update()
        {
            if (!_gameStateMachine.NewPhase) return;
            _gameStateMachine.NewPhase = false;
            switch (_gameStateMachine.CurrentGameState)
            {
                case GameStateMachine.GameState.GameStart:
                    SetUpGame();
                    _gameStateMachine.NextPhase();
                    break;
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

        public event StartTurnDelegate StartTurn;
        public event EndTurnDelegate EndTurn;

        private void SetUpGame()
        {
            _boardController = Instantiate(boardControllerPrefab, transform.position, Quaternion.identity);
            _boardController.Populate(OnSelection);
            _characterOrder.StartCircle(_boardController);
            StartTurn += _pathFinder.Allow;
            EndTurn += _pathFinder.Cleanup;
            Debug.Log("Set up game done");
        }
        
        private void OnStartTurn()
        {
            StartTurn?.Invoke(_characterOrder.CurrentCreature);
        }

        private void OnEndTurn()
        {
            EndTurn?.Invoke();
        }

        private void OnSelection(TileController tile)
        {
            _characterOrder.CurrentCreature.Move(tile);
            _gameStateMachine.NextPhase();
        }
    }

    public delegate void StartTurnDelegate(CreatureController currentCharacter);

    public delegate void EndTurnDelegate();
}