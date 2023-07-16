using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.View.Grid {
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(GoldBarViewContainer))]
    public class GoldBarViewController : MonoBehaviour, IDragHandler, IEndDragHandler {
        public event Action<GoldBarViewController> DragEnded;

        [SerializeField] private GoldBarViewContainer _container;
        private RectTransform _root;

        public void Init(RectTransform root) {
            _root = root;
        }
        
        public void OnDrag(PointerEventData eventData) {
            SetDragPosition(eventData);
        }
        
        public void OnEndDrag(PointerEventData eventData) {
            DragEnded?.Invoke(this);
        }

        private void SetDragPosition(PointerEventData data) {
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_root, data.position, data.pressEventCamera, out var globalMousePos))
            {
                _container.Root.position = globalMousePos;
            }
        }
    }
}
