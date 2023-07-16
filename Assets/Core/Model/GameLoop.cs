using System.Collections.Generic;
using PoorMansECS.Systems;

namespace Core.Model {
    public class GameLoop {
        private readonly HashSet<IUpdateable> _updateables;

        public GameLoop() {
            _updateables = new HashSet<IUpdateable>();
        }

        public void Update(float deltaTime) {
            foreach (var updateable in _updateables) {
                updateable.Update(deltaTime);
            }
        }
        
        public void AddUpdateable(IUpdateable updateable) {
            _updateables.Add(updateable);
        }

        public void Clear() {
            _updateables.Clear();
        }
    }
}
