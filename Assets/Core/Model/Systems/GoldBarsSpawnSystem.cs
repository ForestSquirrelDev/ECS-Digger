using System.Collections.Generic;
using Core.Model.Components;
using Core.Model.Configs;
using Core.Model.Entities;
using Core.Model.Entities.RuntimeCreated;
using Core.Utils;
using PoorMansECS;
using PoorMansECS.Systems;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Random = System.Random;

namespace Core.Model.Systems {
    public class GoldBarsSpawnSystem : SystemBase, ILoadHandlerSystem {
        private readonly Random _random;
        private GoldBarsConfig _config;

        public GoldBarsSpawnSystem(SystemsContext context) : base(context) {
            _random = new Random();
        }
        
        public void Load(Dictionary<string, object> json) {
            var goldBars = json.GetOrCreateNodeList("gold_bars");
            foreach (var _ in goldBars) {
                CreateGoldBar(new Vector2Int(0, 0), _context.World);
            }
        }

        protected override void OnStart() {
            _config = Addressables.LoadAssetAsync<GoldBarsConfig>(AddressablesConsts.DefaultGoldBarsConfig).WaitForCompletion();
        }

        protected override void OnUpdate(float delta) {
            var cells = _context.World.Entities.GetFirst<GridEntity>().GetComponent<GridCellsComponent>().GridCells;
            foreach (var cell in cells) {
                if (!cell.HasComponent<Tag_RecentlyDiggedCell>()) 
                    continue;

                var random = _random.Next(0, 100);
                var shouldSpawnGoldBar = random < _config.DropChance;
                if (!shouldSpawnGoldBar) {
                    cell.RemoveComponent<Tag_RecentlyDiggedCell>();
                    continue;
                }
                CreateGoldBar(cell.GetComponent<CellPositionComponent>().GridSpacePosition, _context.World);
                cell.RemoveComponent<Tag_RecentlyDiggedCell>();
            }
        }
        
        private void CreateGoldBar(Vector2Int position, World world) {
            var goldBar = world.CreateEntity<GoldBarEntity>();
            var gridPosition = position;
            goldBar.SetComponent(new GoldBarPositionComponent(position));
        }

        protected override void OnStop() { }
    }
}
