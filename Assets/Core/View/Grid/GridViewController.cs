using PoorMansECS;

namespace Core.View.Grid {
    public class GridViewController {
        private readonly GridCellsViewController _cellsViewController;
        private readonly GoldBarsViewController _goldBarsViewController;

        public GridViewController(World world, GridViewContainer container) {
            _cellsViewController = new GridCellsViewController(container, world);
            _goldBarsViewController = new GoldBarsViewController(world, container, _cellsViewController);
        }
    
        public void Init() {
            _cellsViewController.Init();
        }

        public void OnUpdate() {
            _cellsViewController.OnUpdate();
            _goldBarsViewController.OnUpdate();
        }

        public void Dispose() {
            _cellsViewController.Dispose();
            _goldBarsViewController.Dispose();
        }
    }
}