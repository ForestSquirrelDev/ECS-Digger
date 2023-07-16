using System.Collections.Generic;
using Core.Utils;
using PoorMansECS.Components;
using UnityEngine;

namespace Core.Model.Components {
    public struct GoldBarPositionComponent : IComponent, ISerializable {
        public Vector2Int Position { get; private set; }

        public GoldBarPositionComponent(Vector2Int position) {
            Position = position;
        }

        public void Serialize(Dictionary<string, object> entityNode) {
            entityNode["gold_bar_position_x"] = Position.x;
            entityNode["gold_bar_position_y"] = Position.y;
        }

        public void Deserialize(Dictionary<string, object> entityNode) {
            var x = entityNode.GetInt("gold_bar_position_x");
            var y = entityNode.GetInt("gold_bar_position_y");
            Position = new Vector2Int(x, y);
        }
    }
}