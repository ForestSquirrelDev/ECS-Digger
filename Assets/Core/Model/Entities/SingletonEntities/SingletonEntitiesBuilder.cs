using Core.Input;
using Core.Model.Components;
using Core.Model.Configs;
using Core.Utils;
using PoorMansECS;
using UnityEngine.AddressableAssets;

namespace Core.Model.Entities.SingletonEntities {
    public class SingletonEntitiesBuilder {
        private readonly World _world;

        public SingletonEntitiesBuilder(World world) {
            _world = world;
        }

        public void Build() {
            BuildPlayer(_world);
            BuildUiInputEntity(_world);
            BuildGameStateTntity(_world);
        }
        
        private void BuildPlayer(World world) {
            var player = world.CreateEntity<PlayerEntity>();
            var playerConfig = LoadPlayerConfig(AddressablesConsts.DefaultPlayerConfig);
            player.SetComponent(new PlayerAvailableShovelsComponent(playerConfig.InitialShovels));
            player.SetComponent(new PlayerCollectedGoldBarsComponent());
        }

        private void BuildUiInputEntity(World world) {
            var uiInputEntity = world.CreateEntity<UIInputEntity>();
            uiInputEntity.SetComponent(new CellTapInputCommands());
        }

        private void BuildGameStateTntity(World world) {
            var gameStateEntity = world.CreateEntity<GameStateEntity>();
            gameStateEntity.SetComponent(new GameStateComponent(GameStateComponent.GameState.None));
        }
        
        private PlayerConfig LoadPlayerConfig(string path) {
            return Addressables.LoadAssetAsync<PlayerConfig>(path).WaitForCompletion();
        }
    }
}