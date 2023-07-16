using UnityEngine;

namespace Core.Model.Configs {
    [CreateAssetMenu(menuName = "ScriptableObjects/Configs/GoldBarsConfig")]
    public class GoldBarsConfig : ScriptableObject {
        [Range(0, 100)]
        public int DropChance = 1;
    }
}
