using System.Collections.Generic;
using Core.Model.Components;
using Core.Utils;
using PoorMansECS.Entities;

namespace Core.Model.Entities {
    public class GridEntity : Entity, ISerializable {
        public void Serialize(Dictionary<string, object> saveJson) {
            var entityNode = new Dictionary<string, object>();
            foreach (var component in _components.Values) {
                if (component is not ISerializable serializable)
                    continue;
                serializable.Serialize(entityNode);
            }
            saveJson["grid_entity"] = entityNode;
        }

        public void Deserialize(Dictionary<string, object> saveJson) {
            var entityNode = saveJson.TryGetNode("grid_entity");
            foreach (var component in _components.Values) {
                if (component is ISerializable serializable)
                    serializable.Deserialize(entityNode);
            }
        }
    }
}