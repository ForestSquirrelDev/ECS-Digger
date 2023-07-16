using System;
using UnityEngine;

namespace Core.View.Grid {
    public class GridCellViewController : MonoBehaviour {
        public event Action<GridCellViewController> ButtonClicked;
        public Vector2Int GridSpacePosition { get; private set; }
        
        [SerializeField] private GridCellViewContainer _container;
        private int _maxDigs;

        private void Awake() {
            _container.Button.onClick.AddListener(OnButtonClick);
        }
        
        public void Init(int availableDigs, int maxDigs, Vector2Int gridSpacePosition) {
            _maxDigs = maxDigs;
            GridSpacePosition = gridSpacePosition;
            Redraw(availableDigs, maxDigs, _container);
        }

        public void OnUpdate(int availableDigs) {
            _container.Button.interactable = availableDigs > 0;
            Redraw(availableDigs, _maxDigs, _container);
        }
        
        private void Redraw(int availableDigs, int maxDigs, GridCellViewContainer container) {
            container.AvailableDigsText.text = availableDigs.ToString();
            container.AvailableDigsText.color = Color.Lerp(container.NoDigsColor, container.MaxDigsColor, availableDigs / maxDigs);
        }

        private void OnButtonClick() {
            ButtonClicked?.Invoke(this);
        }

        private void OnDestroy() {
            _container.Button.onClick.RemoveListener(OnButtonClick);
        }
    }
}