using System.Collections.Generic;
using System.IO;
using Core.Model.Components;
using UnityEngine;

namespace Core.Model {
    public class SaveController {
        public void Save(GameModel model) {
            var entities = model.World.Entities.GetAllEntitiesFlatListed();
            var saveJson = new Dictionary<string, object>();
            foreach (var entity in entities) {
                if (entity is ISerializable serializable) {
                    serializable.Serialize(saveJson);
                }
            }
            File.WriteAllText(Application.persistentDataPath, fastJSON.JSON.ToNiceJSON(saveJson));
        }
    }
}
