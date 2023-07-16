using System;
using System.Collections.Generic;
using System.Linq;

namespace PoorMansECS.Systems {
    public class Systems : IUpdateable{
        private readonly Dictionary<Type, ISystem> _systems = new Dictionary<Type, ISystem>();

        public void Start() {
            foreach (var system in _systems.Values) {
                system.Start();
            }
        }

        public void Update(float deltaTime) {
            foreach (var system in _systems.Values) {
                system.Update(deltaTime);
            }
        }

        internal void Stop() {
            foreach (var system in _systems.Values) {
                system.Stop();
            }
        }

        internal void Add(ISystem system) {
            _systems.Add(system.GetType(), system);
        }

        internal void Remove<T>() where T: ISystem {
            if (!_systems.TryGetValue(typeof(T), out var system))
                return;
            system.Stop();
            _systems.Remove(typeof(T));
        }

        public T Get<T>() where T : ISystem {
            return (T)_systems[typeof(T)];
        }

        public IReadOnlyList<ISystem> GetAll() {
            return _systems.Values.ToList();
        }

        public bool TryGet<T>(out T system) where T: ISystem {
            if (_systems.TryGetValue(typeof(T), out var innerSystem)) {
                system = (T)innerSystem;
                return true;
            }
            system = default;
            return false;
        }

        public bool Contains<T>() where T: ISystem {
            return _systems.ContainsKey(typeof(T));
        }
    }
}
