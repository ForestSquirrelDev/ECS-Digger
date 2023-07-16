using Core.Input;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.View {
    public class EndGameUIContainer : MonoBehaviour {
        public GameObject Root;
        public Button RestartButton;
        public TMP_Text StateText;
        public ManagerInputCommandsPipe ManagerInputCommands;
    }
}
