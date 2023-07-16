using PoorMansECS.Entities;
using PoorMansECS.Systems;
using System;

namespace PoorMansECS {
    public class World : IUpdateable {
        public Entities.Entities Entities { get; }
        public Systems.Systems Systems { get; }
        public SystemsEventBus EventBus { get; }

        private readonly SystemsContext _systemsContext;

        public World() {
            Entities = new Entities.Entities();
            Systems = new Systems.Systems();
            EventBus = new SystemsEventBus();
            _systemsContext = new SystemsContext(this, EventBus);
        }

        public void Start() {
            Systems.Start();
        }

        public void Update(float delta) {
            Systems.Update(delta);
        }

        public T CreateSystem<T>() where T : SystemBase {
            if (Systems.TryGet<T>(out var system))
                return system;
            system = Activator.CreateInstance(typeof(T), _systemsContext) as T;
            Systems.Add(system);
            return system;
        }

        public T CreateEntity<T>() where T : IEntity, new() {
            var entity = new T();
            Entities.Add(entity);
            return entity;
        }

        public void RemoveAllEntitiesOfType<T>() where T : IEntity {
            Entities.RemoveAllEntitiesOfType<T>();
        }

        public void RemoveEntity(IEntity entity) {
            Entities.RemoveEntity(entity);
        }

        public void RemoveAllEntities() {
            Entities.RemoveAll();
        }

        public void Stop() {
            Systems.Stop();
        }
    }
}