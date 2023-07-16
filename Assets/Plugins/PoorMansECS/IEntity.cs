using PoorMansECS.Components;

namespace PoorMansECS.Entities {
    public interface IEntity {
        void SetComponent(IComponent component);
        void RemoveComponent<TComponent>() where TComponent : IComponent;
        TComponent GetComponent<TComponent>() where TComponent : IComponent;
        bool TryGetComponent<T>(out T componentData) where T : IComponent;
        bool HasComponent<T>() where T : IComponent;
    }
}
