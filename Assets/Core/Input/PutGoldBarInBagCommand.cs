using PoorMansECS.Components;
using PoorMansECS.Entities;

namespace Core.Input {
    public readonly struct PutGoldBarInBagCommand : IComponent {
        public IEntity GoldBarEntity { get; }

        public PutGoldBarInBagCommand(IEntity goldBarEntity) {
            GoldBarEntity = goldBarEntity;
        }
    }
}
