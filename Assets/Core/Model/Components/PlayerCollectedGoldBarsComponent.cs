using System.Collections.Generic;
using Core.Utils;
using PoorMansECS.Components;

namespace Core.Model.Components {
    public struct PlayerCollectedGoldBarsComponent : IComponent, ISerializable {
        public int CollectedGoldBars { get; private set; }

        public PlayerCollectedGoldBarsComponent(int collectedGoldBars) {
            CollectedGoldBars = collectedGoldBars;
        }

        public void Serialize(Dictionary<string, object> entityNode) {
            entityNode["collected_gold_bars"] = CollectedGoldBars;
        }

        public void Deserialize(Dictionary<string, object> entityNode) {
            CollectedGoldBars = entityNode.GetInt("collected_gold_bars");
        }
    }
}
