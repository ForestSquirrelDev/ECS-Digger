using Core.Model.Configs;
using Core.Utils;
using PoorMansECS.Systems;
using UnityEngine.AddressableAssets;

namespace Core.Model {
    public class PeriodicSaveHandler : IUpdateable {
        private readonly SaveLoadController _saveLoadController;
        private PeriodicSaveConfig _config;

        private float _timeSinceLastUpdate;
        
        public PeriodicSaveHandler(SaveLoadController saveLoadController) {
            _saveLoadController = saveLoadController;
        }

        public void Start() {
            _config = Addressables.LoadAssetAsync<PeriodicSaveConfig>(AddressablesConsts.DefaultPeriodicSaveHandlerConfig).WaitForCompletion();
        }

        public void Update(float delta) {
            _timeSinceLastUpdate += delta;
            if (_timeSinceLastUpdate > _config.SaveFrequencySeconds) {
                _saveLoadController.Save();
                _timeSinceLastUpdate = 0;
            }
        }
    }
}
