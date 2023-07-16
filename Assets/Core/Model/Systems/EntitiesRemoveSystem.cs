using Core.Input;
using Core.Model.Entities.SingletonEntities;
using PoorMansECS.Systems;

namespace Core.Model.Systems {
    public class EntitiesRemoveSystem : SystemBase {
        public EntitiesRemoveSystem(SystemsContext context) : base(context) { }
        
        protected override void OnUpdate(float delta) {
            var uiInput = _context.World.Entities.GetFirst<UIInputEntity>();
            var removeCommand = uiInput.GetComponent<RemoveEntityCommand>();
            _context.World.RemoveEntity(removeCommand.EntityToRemove);
        }

        protected override void OnStart() { }
        protected override void OnStop() { }
    }
}
