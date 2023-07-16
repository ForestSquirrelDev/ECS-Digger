using System.Collections.Generic;
using Core.Model.Components;
using PoorMansECS.Entities;

namespace Core.Model.Entities.RuntimeCreated {
    public class GoldBarEntity : Entity, ISerializable {
        public void Serialize(Dictionary<string, object> saveJson) {
            /*var entityNode = new Dictionary<string, object>();
            foreach (var component in _components.Values) {
                if (component is not ISerializable serializable)
                    continue;
                serializable.Serialize(entityNode);
            }
            saveJson["player_entity"] = entityNode;*/
        }

        public void Deserialize(Dictionary<string, object> saveJson) {
            //throw new System.NotImplementedException();
        }
    }
}