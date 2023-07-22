using Core.Input;
using Core.Model;
using Core.View;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Manager {
    public class GameManager : MonoBehaviour {
        [SerializeField] private GameModel _model;
        [SerializeField] private MainUIController _view;
        [SerializeField] private ManagerInputCommandsPipe _incomingCommands;

        private void Awake() {
            _model.Init();
            _view.Init();
        }

        private void Update() {
            for (int i = _incomingCommands.Commands.Count; i > 0; i--) {
                var command = _incomingCommands.Commands.Dequeue();
                ProcessCommand(command);
            }
        }

        private void ProcessCommand(IManagerInputCommand command) {
            if (command is RestartGameCommand) {
                Restart();
            }
        }

        private void Restart() {
            _view.Dispose();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}