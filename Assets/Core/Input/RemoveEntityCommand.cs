using PoorMansECS.Components;
using PoorMansECS.Entities;

namespace Core.Input {
    public readonly struct RemoveEntityCommand : IComponent {
        public IEntity EntityToRemove { get; }

        public RemoveEntityCommand(IEntity entityToRemove) {
            EntityToRemove = entityToRemove;
        }
    }
}
