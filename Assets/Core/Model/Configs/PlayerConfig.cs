using UnityEngine;

namespace Core.Model.Configs {
    [CreateAssetMenu(menuName = "ScriptableObjects/Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject {
        public int InitialShovels = 20;
        public int MaxGoldBars = 3;
    }
}