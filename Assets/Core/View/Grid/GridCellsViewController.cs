using Core.Input;
using Core.Model.Components;
using Core.Model.Entities;
using Core.Model.Entities.SingletonEntities;
using Core.Utils;
using PoorMansECS;
using PoorMansECS.Entities;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace Core.View.Grid {
    public class GridCellsViewController {
        private readonly GridViewContainer _container;
        private readonly World _world;
        private GridCellViewController[,] _cellViews;

        public GridCellsViewController(GridViewContainer container, World world) {
            _container = container;
            _world = world;
        }

        public void Init() {
            var grid = _world.Entities.GetFirst<GridEntity>();
            var gridSize = grid.GetComponent<GridSizeComponent>().Size;
            
            _cellViews = BuildCells(_container, grid, gridSize);
            FitGridLayoutToCellsCount(_container.GridLayoutGroup, gridSize);
            
            Attach(_cellViews);
            LayoutRebuilder.ForceRebuildLayoutImmediate(_container.CellsRoot);
        }

        public void OnUpdate() {
            var modelGridCells = _world.Entities.GetFirst<GridEntity>().GetComponent<GridCellsComponent>().GridCells;
            UpdateCells(modelGridCells, _cellViews);
        }

        public Transform GetCellViewTransform(Vector2Int gridPosition) {
            return _cellViews[gridPosition.x, gridPosition.y].transform;
        }

        public void Dispose() {
            Detach(_cellViews);
        }
        
        private void UpdateCells(GridCellEntity[,] gridCells, GridCellViewController[,] cellViews) {
            for (var x = 0; x < cellViews.GetLength(0); x++) {
                for (var y = 0; y < cellViews.GetLength(1); y++) {
                    var cellView = cellViews[x, y];
                    var cellModel = gridCells[x, y];
                    cellView.OnUpdate(cellModel.GetComponent<CellAvailableDigsComponent>().AvailableDigs);
                }
            }
        }

        private GridCellViewController[,] BuildCells(GridViewContainer container, IEntity grid, Vector2Int gridSize) {
            var allCells = new GridCellViewController[gridSize.x, gridSize.y];
            var modelGridCells = grid.GetComponent<GridCellsComponent>().GridCells;
            
            for (int x = 0; x < gridSize.x; x++) {
                for (int y = 0; y < gridSize.y; y++) {
                    var cellModel = modelGridCells[x, y];
                    var cellView = BuildSingleCell(container.CellsRoot, cellModel);
                    allCells[x, y] = cellView;
                }
            }

            return allCells;
        }

        private GridCellViewController BuildSingleCell(Transform root, IEntity cellModel) {
            var cellPrefab = LoadCellPrefab(AddressablesConsts.CellPrefab);
            var cellInstance = Object.Instantiate(cellPrefab, root);

            var cellGridPosition = cellModel.GetComponent<CellPositionComponent>().GridSpacePosition;
            var availableDigs = cellModel.GetComponent<CellAvailableDigsComponent>().AvailableDigs;
            
            var cellController = cellInstance.GetComponent<GridCellViewController>();
            var maxDigs = cellModel.GetComponent<MaxDigsComponent>().MaxDigs;
            cellController.Init(availableDigs, maxDigs, cellGridPosition);
            
            return cellController;
        }

        private GameObject LoadCellPrefab(string path) {
            return Addressables.LoadAssetAsync<GameObject>(path).WaitForCompletion();
        }

        private void FitGridLayoutToCellsCount(GridLayoutGroup gridLayoutGroup, Vector2Int modelGridSize) {
            gridLayoutGroup.constraintCount = modelGridSize.y;
        }
        
                
        private void Attach(GridCellViewController[,] allCells) {
            foreach (var cell in allCells) {
                cell.ButtonClicked += OnSingleCellClicked;
            }
        }
        
        private void OnSingleCellClicked(GridCellViewController cell) {
            var uiInputEntity = _world.Entities.GetFirst<UIInputEntity>();
            uiInputEntity.GetComponent<CellTapInputCommands>().Enqueue(cell.GridSpacePosition);
        }
        
        private void Detach(GridCellViewController[,] allCells) {
            foreach (var cell in allCells) {
                cell.ButtonClicked -= OnSingleCellClicked;
            }
        }
    }
}
