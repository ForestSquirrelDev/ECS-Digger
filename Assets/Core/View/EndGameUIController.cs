using Core.Input;
using Core.Model;
using Core.Model.Components;
using Core.Model.Entities.SingletonEntities;

namespace Core.View {
    public class EndGameUIController {
        private readonly EndGameUIContainer _container;
        private readonly GameModel _model;

        public EndGameUIController(EndGameUIContainer container, GameModel model) {
            _container = container;
            _model = model;
        }
        
        public void Init() {
            _container.RestartButton.onClick.AddListener(Restart);
        }

        public void Update() {
            if (!_model.World.Entities.TryGetFirst(out GameStateEntity gameState))
                return;
            
            if (_container.Root.activeSelf)
                return;
            
            var stateComponent = gameState.GetComponent<GameStateComponent>();
            if (stateComponent.State == GameStateComponent.GameState.Lose) {
                DrawLose(_container);
                return;
            }
            if (stateComponent.State == GameStateComponent.GameState.Win) {
                DrawWin(_container);
            }
        }

        private void DrawLose(EndGameUIContainer container) {
            container.Root.SetActive(true);
            container.StateText.text = "You lose";
        }

        private void DrawWin(EndGameUIContainer container) {
            container.Root.SetActive(true);
            container.StateText.text = "You win";
        }
        
        private void Restart() {
            _container.Root.SetActive(false);
            _container.ManagerInputCommands.Commands.Enqueue(new RestartGameCommand());
        }
    }
}