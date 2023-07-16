using Core.Input;
using Core.Model.Components;
using Core.Model.Entities.SingletonEntities;
using PoorMansECS;
using PoorMansECS.Systems;

namespace Core.Model.Systems {
    public class PlayerGoldBarsInBagPutterSystem : SystemBase {
        public PlayerGoldBarsInBagPutterSystem(SystemsContext context) : base(context) { }

        protected override void OnStart() { }

        protected override void OnUpdate(float delta) {
            var uiInput = _context.World.Entities.GetFirst<UIInputEntity>();
            if (!uiInput.TryGetComponent(out PutGoldBarInBagCommand putGoldBarInBagCommand))
                return;

            IncrementPlayerBars(_context.World);
            _context.World.RemoveEntity(putGoldBarInBagCommand.GoldBarEntity);
            uiInput.RemoveComponent<PutGoldBarInBagCommand>();
        }

        private void IncrementPlayerBars(World world) {
            var player = world.Entities.GetFirst<PlayerEntity>();
            var playerBag = player.GetComponent<PlayerCollectedGoldBarsComponent>().CollectedGoldBars;
            player.SetComponent(new PlayerCollectedGoldBarsComponent(playerBag + 1));
        }

        protected override void OnStop() { }
    }
}
