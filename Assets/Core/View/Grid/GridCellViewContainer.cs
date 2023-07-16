using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.View.Grid {
    public class GridCellViewContainer : MonoBehaviour {
        public Color MaxDigsColor = Color.green;
        public Color NoDigsColor = Color.red;
        
        public Button Button;
        public TMP_Text AvailableDigsText;
    }
}
