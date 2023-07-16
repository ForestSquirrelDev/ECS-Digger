using PoorMansECS.Components;
using System;
using System.Collections.Generic;

namespace PoorMansECS.Entities {
    public abstract class Entity : IEntity {
        protected readonly Dictionary<Type, IComponent> _components = new Dictionary<Type, IComponent>();

        public void SetComponent(IComponent component) {
            _components[component.GetType()] = component;
        }

        public void RemoveComponent<T>() where T : IComponent {
            _components.Remove(typeof(T));
        }

        public T GetComponent<T>() where T : IComponent {
            return (T)_components[typeof(T)];
        }

        public bool TryGetComponent<T>(out T componentData) where T : IComponent {
            if (_components.TryGetValue(typeof(T), out var component)) {
                componentData = (T)component;
                return true;
            }
            componentData = default;
            return false;        
        }

        public bool HasComponent<T>() where T : IComponent {
            return _components.ContainsKey(typeof(T));
        }
    }
}
