using PoorMansECS.Components;

namespace Core.Model.Components {
    public readonly struct MaxDigsComponent : IComponent {
        public int MaxDigs { get; }

        public MaxDigsComponent(int maxDigs) {
            MaxDigs = maxDigs;
        }
    }
}