using Core.Model;
using Core.Model.Components;
using Core.Model.Entities.SingletonEntities;

namespace Core.View.Grid {
    public class PlayerShovelsCounter {
        private readonly PlayerShovelsCounterContainer _container;
        private readonly GameModel _gameModel;

        public PlayerShovelsCounter(PlayerShovelsCounterContainer container, GameModel gameModel) {
            _container = container;
            _gameModel = gameModel;
        }

        public void OnUpdate() {
            if (!_gameModel.World.Entities.TryGetFirst(out PlayerEntity player))
                return;

            var availableShovels = player.GetComponent<PlayerAvailableShovelsComponent>().AvailableShovels;
            _container.CounterText.text = $"Available shovels: {availableShovels}";
        }
    }
}
