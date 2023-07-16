using System;
using System.Collections.Generic;
using System.Linq;

namespace PoorMansECS.Entities {
    public class Entities {
        private readonly Dictionary<Type, List<IEntity>> _entities = new Dictionary<Type, List<IEntity>>();

        public T GetFirst<T>() where T : IEntity {
            var entityType = typeof(T);
            var entitiesList = _entities[entityType];
            return (T)entitiesList[0];
        }

        public T GetFirst<T>(Func<IEntity, bool> condition) where T: IEntity {
            var entityType = typeof(T);
            var entitiesList = _entities[entityType];
            foreach (var entity in entitiesList) {
                if (condition(entity))
                    return (T)entity;
            }
            return default;
        }

        public bool TryGetFirst<T>(out T entity) where T : IEntity {
            var entityType = typeof(T);
            if (!_entities.TryGetValue(entityType, out var entitiesList)) {
                entity = default;
                return false;
            }
            if (entitiesList.Count <= 0) {
                entity = default;
                return false;
            }

            entity = (T)entitiesList[0];
            return true;
        }

        public IReadOnlyList<IEntity> GetAll<T>() where T : IEntity {
            var entityType = typeof(T);
            var entitiesList = _entities[entityType];
            return entitiesList;
        }

        public bool TryGetAll<T>(out IReadOnlyList<IEntity> entitiesList) where T: IEntity {
            if (!_entities.TryGetValue(typeof(T), out var list)) {
                entitiesList = default;
                return false;
            }
            entitiesList = list;
            return true;
        }

        public IReadOnlyList<IEntity> GetAllEntitiesFlatListed() {
            return _entities.Values.SelectMany(_ => _).Cast<IEntity>().ToList();
        }

        internal void Add(IEntity entity) {
            var entityType = entity.GetType();
            if (!_entities.TryGetValue(entityType, out var entitiesSet)) {
                entitiesSet = new List<IEntity>();
                _entities[entityType] = entitiesSet;
            }

            entitiesSet.Add(entity);
        }

        internal void RemoveAllEntitiesOfType<T>() where T : IEntity {
            _entities.Remove(typeof(T));
        }

        internal void RemoveEntity(IEntity entity) {
            var entityType = entity.GetType();
            var entitiesList = _entities[entityType];
            entitiesList.Remove(entity);
        }

        internal void RemoveAll() {
            _entities.Clear();
        }
    }
}
