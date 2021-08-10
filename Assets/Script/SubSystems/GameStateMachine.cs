using System;

namespace Script.SubSystems
{
    public class GameStateMachine
    {
        public enum GameState
        {
            PRETurnPhase,
            StartTurnPhase,
            ActionPhase,
            EndTurnPhase,
            ExitGame
        }

        public GameStateMachine()
        {
            CurrentGameState = GameState.PRETurnPhase;
            NewPhase = false;
        }

        public GameState CurrentGameState { get; private set; }
        public bool NewPhase { get; set; }

        public void NextPhase()
        {
            switch (CurrentGameState)
            {
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

            NewPhase = true;
        }
    }
}