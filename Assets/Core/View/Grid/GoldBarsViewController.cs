using System.Collections.Generic;
using System.Linq;
using Core.Input;
using Core.Model.Components;
using Core.Model.Entities.RuntimeCreated;
using Core.Model.Entities.SingletonEntities;
using Core.Utils;
using PoorMansECS;
using PoorMansECS.Entities;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace Core.View.Grid {
    public class GoldBarsViewController {
        private readonly GridViewContainer _container;
        private readonly List<(IEntity model, GoldBarViewController view)> _goldBarViewControllers;
        private readonly GridCellsViewController _cellsViewController;
        private readonly World _world;

        public GoldBarsViewController(World world, GridViewContainer container, GridCellsViewController cellsViewController) {
            _world = world;
            _container = container;
            _goldBarViewControllers = new List<(IEntity, GoldBarViewController)>();
            _cellsViewController = cellsViewController;
        }

        public void OnLateUpdate() {
            SpawnGoldBarsIfNeeded(_world, _goldBarViewControllers, _cellsViewController);
        }

        public void Dispose() {
            foreach (var goldBar in _goldBarViewControllers) {
                goldBar.view.DragEnded -= OnBarDragEnded;
            }
            _goldBarViewControllers.Clear();
        }

        private void SpawnGoldBarsIfNeeded(World world, List<(IEntity model, GoldBarViewController view)> goldBarViews, GridCellsViewController cellsViewController) {
            if (!world.Entities.TryGetAll<GoldBarEntity>(out var goldBars))
                return;
            foreach (var goldBarModel in goldBars) {
                if (goldBarViews.Any(bar => bar.model == goldBarModel)) continue;

                var goldBarPrefab = LoadGoldBarPrefab(AddressablesConsts.GoldBarPrefab);
                var goldBarController = BuildSingleGoldBar(cellsViewController, goldBarModel, goldBarPrefab);
                goldBarController.DragEnded += OnBarDragEnded;
                goldBarViews.Add((goldBarModel, goldBarController));
            }
        }
        
        private GoldBarViewController BuildSingleGoldBar(GridCellsViewController cellsViewController, IEntity goldBarModel, GameObject goldBarPrefab) {
            var position = goldBarModel.GetComponent<GoldBarPositionComponent>().Position;
            var cellTransform = cellsViewController.GetCellViewTransform(new Vector2Int(position.x, position.y));
            var goldBarController = BuildGoldBar(goldBarPrefab, cellTransform.transform.position, _container.Root);
            return goldBarController;
        }

        private GoldBarViewController BuildGoldBar(GameObject prefab, Vector3 position, RectTransform root) {
            var instance = Object.Instantiate(prefab, position, Quaternion.identity, root);
            var controller = instance.GetComponentInChildren<GoldBarViewController>();
            controller.Init(root);
            return controller;
        }

        private GameObject LoadGoldBarPrefab(string path) {
            return Addressables.LoadAssetAsync<GameObject>(path).WaitForCompletion();
        }
        
        private void OnBarDragEnded(GoldBarViewController goldBarViewController) {
            var isInsideGoldBag = (goldBarViewController.transform as RectTransform).IsInsideWithAnyCorner(_container.GoldBarsBagZone);
            if (!isInsideGoldBag)
                return;
            
            PutBarInBag(_world, goldBarViewController, _goldBarViewControllers);
        }

        private void PutBarInBag(World world, GoldBarViewController goldBarViewController, List<(IEntity model, GoldBarViewController view)> barViewControllers) {
            var uiInput = world.Entities.GetFirst<UIInputEntity>();
            
            var correspondingExistingGoldBarItemIndex = barViewControllers.IndexOf(goldBar => goldBar.view == goldBarViewController);
            var correspondingItem = barViewControllers[correspondingExistingGoldBarItemIndex];
            var putGoldBarInBagCommand = new PutGoldBarInBagCommand(correspondingItem.model);
            uiInput.SetComponent(putGoldBarInBagCommand);
            
            CleanupGoldBar(correspondingItem);
        }
        
        private void CleanupGoldBar((IEntity model, GoldBarViewController view) correspondingItem) {
            correspondingItem.view.DragEnded -= OnBarDragEnded;
            Object.Destroy(correspondingItem.view.transform.parent.gameObject);
        }
    }
}
