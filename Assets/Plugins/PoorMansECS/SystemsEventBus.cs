using System;
using System.Collections.Generic;

namespace PoorMansECS.Systems {
    public class SystemsEventBus {
        private readonly Dictionary<Type, HashSet<ISystemsEventListener>> _eventListeners 
            = new Dictionary<Type, HashSet<ISystemsEventListener>>();

        public void Subscribe<T>(ISystemsEventListener listener) where T : ISystemEvent {
            var eventType = typeof(T);
            if (!_eventListeners.ContainsKey(eventType))
                _eventListeners[eventType] = new HashSet<ISystemsEventListener>();
            _eventListeners[eventType].Add(listener);
        }

        public void Unsubscribe<T>(ISystemsEventListener listener) where T : ISystemEvent {
            var eventType = typeof(T);
            if (_eventListeners.ContainsKey(eventType))
                _eventListeners[eventType].Remove(listener);
        }

        public void SendEvent<T>(T systemEvent) where T : ISystemEvent {
            if (!_eventListeners.TryGetValue(typeof(T), out var listeners))
                return;
            foreach (var listener in listeners) {
                listener.ReceiveEvent(systemEvent);
            }
        }
    }
}