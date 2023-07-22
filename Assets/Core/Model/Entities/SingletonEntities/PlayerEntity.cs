using System.Collections.Generic;
using Core.Model.Components;
using Core.Utils;
using PoorMansECS.Entities;

namespace Core.Model.Entities.SingletonEntities {
    public class PlayerEntity : Entity, ISerializable {
        public void Serialize(Dictionary<string, object> saveJson) {
            var entityNode = new Dictionary<string, object>();
            foreach (var component in _components.Values) {
                if (component is not ISerializable serializable)
                    continue;
                serializable.Serialize(entityNode);
            }
            saveJson["player_entity"] = entityNode;
        }

        public void Deserialize(Dictionary<string, object> saveJson) {
            var entityNode = saveJson.TryGetNode("player_entity");
            foreach (var component in _components.Values) {
                if (component is not ISerializable serializable)
                    continue;
                serializable.Deserialize(entityNode);
            }
        }
    }
}