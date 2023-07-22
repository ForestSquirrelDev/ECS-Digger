using Core.Model.Entities.SingletonEntities;
using Core.Model.Systems;
using PoorMansECS;
using UnityEngine;

namespace Core.Model {
    public class GameModel : MonoBehaviour {
        public World World { get; } = new();
        
        private readonly GameLoop _gameLoop = new();

        private SaveLoadController _saveLoadController;
        private PeriodicSaveHandler _periodicSaveHandler;
        
        public void Init() {
            _saveLoadController = new SaveLoadController(World);
            _periodicSaveHandler = new PeriodicSaveHandler(_saveLoadController);
            _gameLoop.AddUpdateable(World);
            _gameLoop.AddUpdateable(_periodicSaveHandler);

            var singletonEntitiesBuilder = new SingletonEntitiesBuilder(World);
            var systemsBuilder = new SystemsBuilder(World);
            singletonEntitiesBuilder.Build();
            systemsBuilder.Build();
            World.Start();
            
            _saveLoadController.Load();
            _periodicSaveHandler.Start();
        }

        private void Update() {
            _gameLoop.Update(Time.deltaTime);
        }
    }
}