using PoorMansECS;

namespace Core.Model.Systems {
    public class SystemsBuilder {
        private readonly World _world;

        public SystemsBuilder(World world) {
            _world = world;
        }

        public void Build() {
            _world.CreateSystem<GridGeneratorSystem>();
            _world.CreateSystem<PlayerDigSystem>();
            _world.CreateSystem<GoldBarsSpawnSystem>();
            _world.CreateSystem<PlayerGoldBarsInBagPutterSystem>();
            _world.CreateSystem<GameStateChangerSystem>();
        }
    }
}