using UnityEngine;

namespace Core.Model.Configs {
    [CreateAssetMenu(menuName = "ScriptableObjects/Configs/PeriodicSaveConfig")]
    public class PeriodicSaveConfig : ScriptableObject {
        public int SaveFrequencySeconds = 3;
    }
}