using System;
using Core.Model.Components;
using Core.Model.Configs;
using Core.Model.Entities;
using Core.Model.Entities.RuntimeCreated;
using Core.Utils;
using PoorMansECS.Systems;
using UnityEngine.AddressableAssets;

namespace Core.Model.Systems {
    public class GoldBarsSpawnSystem : SystemBase {
        private readonly Random _random;
        private GoldBarsConfig _config;

        public GoldBarsSpawnSystem(SystemsContext context) : base(context) {
            _random = new Random();
        }

        protected override void OnStart() {
            _config = Addressables.LoadAssetAsync<GoldBarsConfig>(AddressablesConsts.DefaultGoldBarsConfig).WaitForCompletion();
        }

        protected override void OnUpdate(float delta) {
            var cells = _context.World.Entities.GetAll<GridCellEntity>();
            foreach (var cell in cells) {
                if (!cell.HasComponent<Tag_RecentlyDiggedCell>()) 
                    continue;

                var random = _random.Next(0, 100);
                var shouldSpawnGoldBar = random < _config.DropChance;
                if (!shouldSpawnGoldBar) {
                    cell.RemoveComponent<Tag_RecentlyDiggedCell>();
                    continue;
                }
                
                var goldBar = _context.World.CreateEntity<GoldBarEntity>();
                var cellPosition = cell.GetComponent<CellPositionComponent>().GridSpacePosition;
                goldBar.SetComponent(new GoldBarPositionComponent(cellPosition));
                cell.RemoveComponent<Tag_RecentlyDiggedCell>();
            }
        }
        
        private GoldBarsConfig LoadGoldBarsConfig(string path) {
            return Addressables.LoadAssetAsync<GoldBarsConfig>(path).WaitForCompletion();
        }
        
        protected override void OnStop() { }
    }
}
