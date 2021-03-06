using System;
using Script.Entity.Creature;
using Script.GameBoard;
using Script.GameBoard.Tile;
using Script.GameController.SubSystems;
using UnityEngine;

namespace Script.GameController
{
    [RequireComponent(
        typeof(CreatureManager))]
    public class GameController : MonoBehaviour
    {
        public BoardController boardControllerPrefab;
        
        private BoardController _boardController;
        private CreatureManager _creatureManager;
        private GameStateMachine _gameStateMachine;
        private PathFinder _pathFinder;
        private Spawner _spawner;
        
        public event StartTurnDelegate StartTurn;
        public event EndTurnDelegate EndTurn;

        private void Awake()
        {
            _creatureManager = GetComponent<CreatureManager>();
            _gameStateMachine = new GameStateMachine();
            _pathFinder = new PathFinder();
            _spawner = new Spawner();
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
                    SpawnCreaturesInGame();
                    _gameStateMachine.NextPhase();
                    break;
                case GameStateMachine.GameState.PRETurnPhase:
                    _creatureManager.NextCreature();
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

        private void SpawnCreaturesInGame()
        {
            for (var i = 0; i < _creatureManager.numberOfTeams; i++)
            {
                Debug.Log(i + " team is ready to spawn");
                var creaturesInTeam = _creatureManager.GETCreaturesInTeam(i);
                var spawnAreaByTeam = _boardController.GETSpawnAreaByTeam(i);
                _spawner.SpawnCreatures(creaturesInTeam, spawnAreaByTeam);
            }

            _creatureManager.CalculateOrder();
        }

        private void SetUpGame()
        {
            _boardController = Instantiate(boardControllerPrefab, transform.position, Quaternion.identity);
            _boardController.Populate(OnSelection);
            StartTurn += _pathFinder.Allow;
            EndTurn += _pathFinder.Cleanup;
            Debug.Log("Set up game done");
        }
        
        private void OnSelection(TileController tile)
        {
            _creatureManager.CurrentCreature.Move(tile);
            _gameStateMachine.NextPhase();
        }

        private void OnStartTurn() => StartTurn?.Invoke(_creatureManager.CurrentCreature);

        private void OnEndTurn() => EndTurn?.Invoke();
    }

    public delegate void StartTurnDelegate(CreatureController currentCreature);

    public delegate void EndTurnDelegate();
}