using Core.Input;
using Core.Model.Components;
using Core.Model.Entities;
using Core.Model.Entities.SingletonEntities;
using PoorMansECS.Entities;
using PoorMansECS.Systems;

namespace Core.Model.Systems {
    public class PlayerDigSystem : SystemBase {
        public PlayerDigSystem(SystemsContext context) : base(context) { }

        protected override void OnUpdate(float delta) {
            var uiInputEntity = _context.World.Entities.GetFirst<UIInputEntity>();
            if (!uiInputEntity.TryGetComponent(out CellTapInputCommands tapInputCommands))
                return;

            var playerEntity = _context.World.Entities.GetFirst<PlayerEntity>();
            var cells = _context.World.Entities.GetFirst<GridEntity>().GetComponent<GridCellsComponent>().GridCells;
            DigCells(tapInputCommands, playerEntity, cells);
        }
        
        private void DigCells(CellTapInputCommands tapInputCommands, IEntity playerEntity, GridCellEntity[,] cells) {
            var availableShovels = playerEntity.GetComponent<PlayerAvailableShovelsComponent>().AvailableShovels;
            if (availableShovels <= 0) return;
            
            for (int i = tapInputCommands.Count; i > 0; i--) {
                var command = tapInputCommands.Dequeue();
                var cell = cells[command.x, command.y];
                var availableDigs = cell.GetComponent<CellAvailableDigsComponent>();
                if (availableDigs.AvailableDigs <= 0) return;

                var newAvailableDigs = new CellAvailableDigsComponent(availableDigs.AvailableDigs - 1);
                cell.SetComponent(newAvailableDigs);
                cell.SetComponent(new Tag_RecentlyDiggedCell());
                playerEntity.SetComponent(new PlayerAvailableShovelsComponent(availableShovels - 1));
            }
        }
        
        protected override void OnStart() { }
        protected override void OnStop() { }
    }
}
