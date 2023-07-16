using Core.Model;
using Core.View.Grid;
using UnityEngine;

namespace Core.View {
    public class MainUIController : MonoBehaviour {
        [SerializeField] private GameModel _model;
        [SerializeField] private MainUIContainer _container;

        private GridViewController _gridView;
        private PlayerShovelsCounter _shovelsCounter;
        private GoldBarsCounter _goldBarsCounter;
        private EndGameUIController _endGameUIController;
    
        public void Init() {
            _gridView = new GridViewController(_model.World, _container.GridViewContainer);
            _shovelsCounter = new PlayerShovelsCounter(_container.ShovelsCounterContainer, _model);
            _goldBarsCounter = new GoldBarsCounter(_container.GoldBarsCounterContainer, _model);
            _endGameUIController = new EndGameUIController(_container.EndGameUIContainer, _model);
            
            _gridView.Init();
            _endGameUIController.Init();
        }

        private void Update() {
            _gridView.OnUpdate();
            _shovelsCounter.OnUpdate();
            _goldBarsCounter.Update();
            _endGameUIController.Update();
        }

        public void Dispose() {
            _gridView.Dispose();
        }
    }
}