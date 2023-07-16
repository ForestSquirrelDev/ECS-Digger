using PoorMansECS.Components;

namespace Core.Model.Components {
    public readonly struct GameStateComponent : IComponent {
        public GameState State { get; }

        public GameStateComponent(GameState state) {
            State = state;
        }
        
        public enum GameState {
            None = 0, InProcess = 1, Lose = 2, Win = 3
        }
    }
}