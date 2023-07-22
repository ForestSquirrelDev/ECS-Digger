using System.Collections.Generic;
using System.IO;
using Core.Model.Components;
using Core.Model.Systems;
using PoorMansECS;
using UnityEngine;

namespace Core.Model {
    public class SaveLoadController {
        private readonly World _world;
        
        private readonly string _path = Path.Combine(Application.dataPath, _fileName);
        private const string _fileName = "save.json";

        private static bool _loaded;

        public SaveLoadController(World world) {
            _world = world;
        }
        
        public void Save() {
            var entities = _world.Entities.GetAllEntitiesFlatListed();
            var saveJson = new Dictionary<string, object>();
            foreach (var entity in entities) {
                if (entity is ISerializable serializable) {
                    serializable.Serialize(saveJson);
                }
            }
            var textFile = fastJSON.JSON.ToNiceJSON(saveJson);
            File.WriteAllText(_path, textFile);
        }

        public void Load() {
            if (!File.Exists(_path)) return;
            if (_loaded) return;
            _loaded = true;
            
            var saveFile = File.ReadAllText(_path);
            var saveJson = fastJSON.JSON.ToObject<Dictionary<string, object>>(saveFile);
            
            foreach (var system in _world.Systems.GetAll()) {
                if (system is ILoadHandlerSystem loadHandlerSystem)
                    loadHandlerSystem.Load(saveJson);
            }
            
            var entities = _world.Entities.GetAllEntitiesFlatListed();
            foreach (var entity in entities) {
                if (entity is ISerializable serializable) {
                    serializable.Deserialize(saveJson);
                }
            }
        }
    }
}
