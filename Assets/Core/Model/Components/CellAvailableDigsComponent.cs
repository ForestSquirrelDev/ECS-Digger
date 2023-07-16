using System.Collections.Generic;
using Core.Utils;
using PoorMansECS.Components;

namespace Core.Model.Components {
    public readonly struct Tag_RecentlyDiggedCell : IComponent { }
    public struct CellAvailableDigsComponent : IComponent, ISerializable {
        public int AvailableDigs { get; private set; }

        public CellAvailableDigsComponent(int availableDigs) {
            AvailableDigs = availableDigs;
        }

        public void Serialize(Dictionary<string, object> entityNode) {
            entityNode["available_digs"] = AvailableDigs;
        }

        public void Deserialize(Dictionary<string, object> entityNode) {
            AvailableDigs = entityNode.GetInt("available_digs");
        }
    }
}
