using UnityEngine;

namespace Core.Model.Configs {
    [CreateAssetMenu(menuName = "ScriptableObjects/Configs/GridConfig")]
    public class GridConfig : ScriptableObject {
        public Vector2Int Size = new Vector2Int(10, 10);
        public int CellDepth = 3;
    }
}