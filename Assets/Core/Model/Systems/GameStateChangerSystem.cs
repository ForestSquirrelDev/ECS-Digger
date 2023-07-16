using Core.Model.Components;
using Core.Model.Configs;
using Core.Model.Entities.SingletonEntities;
using Core.Utils;
using PoorMansECS.Entities;
using PoorMansECS.Systems;
using UnityEngine.AddressableAssets;

namespace Core.Model.Systems {
    public class GameStateChangerSystem : SystemBase {
        private PlayerConfig _playerConfig;
        
        public GameStateChangerSystem(SystemsContext context) : base(context) { }

        protected override void OnStart() {
            _playerConfig = Addressables.LoadAssetAsync<PlayerConfig>(AddressablesConsts.DefaultPlayerConfig).WaitForCompletion();
            _context.World.Entities.GetFirst<GameStateEntity>().SetComponent(new GameStateComponent(GameStateComponent.GameState.InProcess));
        }

        protected override void OnUpdate(float delta) {
            var player = _context.World.Entities.GetFirst<PlayerEntity>();
            var gameState = _context.World.Entities.GetFirst<GameStateEntity>();
            if (gameState.GetComponent<GameStateComponent>().State != GameStateComponent.GameState.InProcess)
                return;

            if (IsLose(player)) {
                gameState.SetComponent(new GameStateComponent(GameStateComponent.GameState.Lose));
                return;
            }
            if (IsWin(player, _playerConfig)) {
                gameState.SetComponent(new GameStateComponent(GameStateComponent.GameState.Win));
            }
        }

        private bool IsLose(IEntity playerEntity) {
            var playerShovels = playerEntity.GetComponent<PlayerAvailableShovelsComponent>();
            return playerShovels.AvailableShovels <= 0;
        }

        private bool IsWin(IEntity playerEntity, PlayerConfig playerConfig) {
            var playerGoldBars = playerEntity.GetComponent<PlayerCollectedGoldBarsComponent>().CollectedGoldBars;
            return playerGoldBars >= playerConfig.MaxGoldBars;
        }

        protected override void OnStop() { }
    }
}
