using System.Collections.Generic;
using System.Linq;
using Core.Model.Components;
using Core.Utils;
using PoorMansECS.Entities;

namespace Core.Model.Entities.RuntimeCreated {
    public class GoldBarEntity : Entity, ISerializable {
        public void Serialize(Dictionary<string, object> saveJson) {
            var goldBarsList = saveJson.GetOrCreateNodeList("gold_bars");
            var entityNode = new Dictionary<string, object>();
            foreach (var component in _components.Values) {
                if (component is ISerializable serializable)
                    serializable.Serialize(entityNode);
            }
            goldBarsList.Add(entityNode);
        }

        public void Deserialize(Dictionary<string, object> saveJson) {
            var goldBarsList = saveJson.GetOrCreateNodeList("gold_bars");
            var anyGoldBarNode = goldBarsList.FirstOrDefault() as Dictionary<string, object>;
            foreach (var component in _components.Values) {
                if (component is ISerializable serializable)
                    serializable.Deserialize(anyGoldBarNode);
            }
            goldBarsList.Remove(anyGoldBarNode);
        }
    }
}