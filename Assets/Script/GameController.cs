using System;
using Script.Entity.Character;
using Script.GameBoard;
using Script.GameBoard.Tile;
using Script.SubSystems;
using UnityEngine;

namespace Script
{
    [RequireComponent(typeof(CreatureManager))]
    public class GameController : MonoBehaviour
    {
        public BoardController boardControllerPrefab;
        private BoardController _boardController;

        private CreatureManager _creatureManager;
        private GameStateMachine _gameStateMachine;
        private PathFinder _pathFinder;
        private Spawner _spawner;

        private void Awake()
        {
            _creatureManager = GetComponent<CreatureManager>();
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
                case GameStateMachine.GameState.SpawnPhase:
                    for (int i = 0; i < _creatureManager.NumberOfTeams; i++)
                    {
                        _spawner.SpawnCreatures(_creatureManager.GETCreaturesInTeam(i),_boardController.GETSpawnAreaByTeam(i));
                    }
                    _gameStateMachine.NextPhase();
                    break;
                case GameStateMachine.GameState.PRETurnPhase:
                    _creatureManager.NextCharacter();
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
            _creatureManager.StartCircle(_boardController);
            StartTurn += _pathFinder.Allow;
            EndTurn += _pathFinder.Cleanup;
            Debug.Log("Set up game done");
        }

        private void OnStartTurn()
        {
            StartTurn?.Invoke(_creatureManager.CurrentCreature);
        }

        private void OnEndTurn()
        {
            EndTurn?.Invoke();
        }

        private void OnSelection(TileController tile)
        {
            _creatureManager.CurrentCreature.Move(tile);
            _gameStateMachine.NextPhase();
        }
    }

    public delegate void StartTurnDelegate(CreatureController currentCharacter);

    public delegate void EndTurnDelegate();
}