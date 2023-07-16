using System.Collections.Generic;
using Core.Utils;
using PoorMansECS.Components;
using UnityEngine;

namespace Core.Model.Components {
    public struct CellPositionComponent : IComponent, ISerializable {
        public Vector2Int GridSpacePosition { get; private set; }
        
        public CellPositionComponent(Vector2Int gridSpacePosition) {
            GridSpacePosition = gridSpacePosition;
        }

        public void Serialize(Dictionary<string, object> entityNode) {
            entityNode["grid_space_position_x"] = GridSpacePosition.x;
            entityNode["grid_space_position_y"] = GridSpacePosition.y;
        }

        public void Deserialize(Dictionary<string, object> entityNode) {
            var x = entityNode.GetInt("grid_space_position_x");
            var y = entityNode.GetInt("grid_space_position_y");
            GridSpacePosition = new Vector2Int(x, y);
        }
    }
}