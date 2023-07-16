using System.Collections.Generic;
using Core.Utils;
using PoorMansECS.Components;

namespace Core.Model.Components {
    public struct PlayerAvailableShovelsComponent : IComponent, ISerializable {
        public int AvailableShovels { get; private set; }

        public PlayerAvailableShovelsComponent(int initialShovels) {
            AvailableShovels = initialShovels;
        }

        public void Serialize(Dictionary<string, object> entityNode) {
            entityNode["available_shovels"] = AvailableShovels;
        }

        public void Deserialize(Dictionary<string, object> entityNode) {
            AvailableShovels = entityNode.GetInt("available_shovels");
        }
    }
}