using System.Collections.Generic;
using Core.Model.Components;
using PoorMansECS.Entities;

namespace Core.Model.Entities {
    public class GridCellEntity : Entity, ISerializable {
        public void Serialize(Dictionary<string, object> entityNode) {
            foreach (var component in _components.Values) {
                if (component is not ISerializable serializable)
                    continue;
                serializable.Serialize(entityNode);
            }
        }

        public void Deserialize(Dictionary<string, object> entityNode) {
            foreach (var component in _components.Values) {
                if (component is not ISerializable serializable)
                    continue;
                serializable.Deserialize(entityNode);
            }
        }
    }
}