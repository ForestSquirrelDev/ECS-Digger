using System.Collections.Generic;
using Core.Model.Components;
using Core.Model.Configs;
using Core.Model.Entities;
using Core.Utils;
using PoorMansECS;
using PoorMansECS.Systems;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.Model.Systems {
    public class GridGeneratorSystem : SystemBase {
        private GridConfig _gridConfig;

        public GridGeneratorSystem(SystemsContext context) : base(context) { }

        protected override void OnStart() {
            var gridConfig = LoadGridConfig(AddressablesConsts.DefaultGridConfig);
            InitGrid(_context.World, gridConfig);
        }
        
        public void Restore(Dictionary<string, object> cellsNode) {
            
        }

        private void InitGrid(World world, GridConfig gridConfig) {
            var gridCellsComponent = BuildGridCells(gridConfig, world);
            var gridEntity = world.CreateEntity<GridEntity>();
            gridEntity.SetComponent(new GridSizeComponent(gridConfig.Size));
            gridEntity.SetComponent(gridCellsComponent);
        }

        private GridCellsComponent BuildGridCells(GridConfig gridConfig, World world) {
            var gridSize = gridConfig.Size;
            var maxDepth = gridConfig.CellDepth;
            
            var gridCells = new GridCellEntity[gridSize.x, gridSize.y];
            for (int x = 0; x < gridSize.x; x++) {
                for (int y = 0; y < gridSize.y; y++) {
                    gridCells[x, y] = BuildCellEntity(world, maxDepth, x, y);
                }
            }
            var gridCellsComponent = new GridCellsComponent(gridCells, gridSize);

            return gridCellsComponent;
        }

        private GridCellEntity BuildCellEntity(World world, int maxDepth, int x, int y) {
            var cell = world.CreateEntity<GridCellEntity>();
            cell.SetComponent(new CellPositionComponent(new Vector2Int(x, y)));
            cell.SetComponent(new CellAvailableDigsComponent(maxDepth));
            cell.SetComponent(new MaxDigsComponent(maxDepth));
            return cell;
        }
        
        private GridConfig LoadGridConfig(string path) {
            return Addressables.LoadAssetAsync<GridConfig>(path).WaitForCompletion();
        }
        
        protected override void OnUpdate(float delta) { }
        protected override void OnStop() { }
    }
}