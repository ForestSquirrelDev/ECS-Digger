using System;
using Core.Model.Entities.SingletonEntities;
using Core.Model.Systems;
using PoorMansECS;
using UnityEngine;

namespace Core.Model {
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

        private void Update() {
            _gameLoop.Update(Time.deltaTime);
            if (UnityEngine.Input.GetKeyDown(KeyCode.H)) {
                _saveController.Save(this);
            }
        }
    }
}