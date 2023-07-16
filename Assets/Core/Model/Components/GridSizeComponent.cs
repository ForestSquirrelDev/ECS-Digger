using PoorMansECS.Components;
using UnityEngine;

namespace Core.Model.Components {
    public readonly struct GridSizeComponent : IComponent {
        public Vector2Int Size { get; }

        public GridSizeComponent(Vector2Int size) {
            Size = size;
        }
    }
}