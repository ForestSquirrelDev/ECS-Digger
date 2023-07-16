using System;
using System.Collections.Generic;
using System.IO;
using Core.Model.Components;
using Core.Model.Entities.SingletonEntities;
using Core.Model.Systems;
using PoorMansECS;
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
    public class GameModel : MonoBehaviour {
        public World World { get; } = new();
        private readonly GameLoop _gameLoop = new();
        private readonly SaveController _saveController = new SaveController();
        
        public void Init() {
            _gameLoop.AddUpdateable(World);

            var singletonEntitiesBuilder = new SingletonEntitiesBuilder(World);
            var systemsBuilder = new SystemsBuilder(World);
            singletonEntitiesBuilder.Build();
            systemsBuilder.Build();
            
            World.Start();
        }

        public void Dispose() {
            _gameLoop.Clear();
            World.RemoveAllEntities();
        }

        public void Save() {
        }

        private void Update() {
            _gameLoop.Update(Time.deltaTime);
            if (UnityEngine.Input.GetKeyDown(KeyCode.H)) {
                _saveController.Save(this);
            }
        }

        private void OnDestroy() {
            Save();
        }
    }
}