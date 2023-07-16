using System.Collections.Generic;
using PoorMansECS.Components;
using UnityEngine;

namespace Core.Input {
    public class CellTapInputCommands : IComponent {
        public int Count => _gridPositionTapCommands.Count;
        
        private readonly Queue<Vector2Int> _gridPositionTapCommands = new();

        public void Enqueue(Vector2Int position) {
            _gridPositionTapCommands.Enqueue(position);
        }

        public Vector2Int Dequeue() {
            return _gridPositionTapCommands.Dequeue();
        }
    }
}