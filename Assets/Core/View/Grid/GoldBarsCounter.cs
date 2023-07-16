using Core.Model;
using Core.Model.Components;
using Core.Model.Entities.SingletonEntities;

namespace Core.View.Grid {
    public class GoldBarsCounter {
        private readonly GoldBarsCounterContainer _container;
        private readonly GameModel _model;

        public GoldBarsCounter(GoldBarsCounterContainer container, GameModel model) {
            _container = container;
            _model = model;
        }
        
        public void Update() {
            if (!_model.World.Entities.TryGetFirst(out PlayerEntity player))
                return;

            var collectedGoldBars = player.GetComponent<PlayerCollectedGoldBarsComponent>().CollectedGoldBars;
            _container.CounterText.text = $"Gold bars: {collectedGoldBars}";
        }
    }
}
