using System;
using System.Collections.Generic;
using Script.Entity.Character;
using Script.GameBoard;
using Script.GameBoard.Tile;
using Script.SubSystems;
using UnityEngine;

namespace Script
{
    //[RequireComponent(typeof(Spawner), typeof(CharacterOrder))]
    public class GameController : MonoBehaviour
    {
        public BoardController boardControllerPrefab;
        public CreatureController currentCharacter;

        //private CharacterOrder _characterOrder;
        //private Spawner _spawner;
        private BoardController _boardController;
        private GameStateMachine _gameStateMachine;

        private void Awake()
        {
            //_spawner = GetComponent<Spawner>();
            //_characterOrder = GetComponent<CharacterOrder>();
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
                        //_characterOrder.NextCharacter();
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
            _boardController.Populate(OnSelection);
            
            //_spawner.SpawnCreature(characters[0],_boardController.get);
        }
        
        private void OnStartTurn()
        {
            StartTurn?.Invoke();
        }

        private void OnEndTurn()
        {
            EndTurn?.Invoke();
        }

        public void OnSelection(TileController tile)
        {
            Debug.Log(tile.name);
            currentCharacter.Move(tile);
        }
    }

    public delegate void StartTurnDelegate();

    public delegate void EndTurnDelegate();
}