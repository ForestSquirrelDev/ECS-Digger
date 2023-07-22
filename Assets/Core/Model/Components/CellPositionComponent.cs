using PoorMansECS.Components;
using UnityEngine;

namespace Core.Model.Components {
    public readonly struct CellPositionComponent : IComponent{
        public Vector2Int GridSpacePosition { get; }
        
        public CellPositionComponent(Vector2Int gridSpacePosition) {
            GridSpacePosition = gridSpacePosition;
        }
    }
}