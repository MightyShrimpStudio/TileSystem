using System;
using UnityEngine;

namespace Script.SubSystems
{
    public class GameStateMachine
    {
        public enum GameState
        {
            GameStart,
            SpawnPhase,
            PRETurnPhase,
            StartTurnPhase,
            ActionPhase,
            EndTurnPhase,
            ExitGame
        }

        public GameStateMachine()
        {
            CurrentGameState = GameState.GameStart;
            Debug.Log("Game state is " + CurrentGameState);
            NewPhase = true;
        }

        public GameState CurrentGameState { get; private set; } = GameState.PRETurnPhase;
        public bool NewPhase { get; set; }

        public void NextPhase()
        {
            switch (CurrentGameState)
            {
                case GameState.GameStart:
                    CurrentGameState = GameState.SpawnPhase;
                    break;
                case GameState.SpawnPhase:
                    CurrentGameState = GameState.PRETurnPhase;
                    break;
                case GameState.PRETurnPhase:
                    CurrentGameState = GameState.StartTurnPhase;
                    break;
                case GameState.StartTurnPhase:
                    CurrentGameState = GameState.ActionPhase;
                    break;
                case GameState.ActionPhase:
                    CurrentGameState = GameState.EndTurnPhase;
                    break;
                case GameState.EndTurnPhase:
                    CurrentGameState = GameState.PRETurnPhase;
                    break;
                case GameState.ExitGame:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Debug.Log("Game state is " + CurrentGameState);
            NewPhase = true;
        }
    }
}