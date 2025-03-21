using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Modules.Entities
{
    public sealed class Entity : MonoBehaviour, IEntity
    {
        [SerializeField, Required, ChildGameObjectsOnly] 
        private GameObjectContext _context;

        private Dictionary<System.Type, object> _componentsCache = new();
        
        public T Get<T>() where T : class
        {
            if (_componentsCache.TryGetValue(typeof(T), out object component))
                return (T)component;
            
            component = _context.Container.Resolve<T>();
            _componentsCache[typeof(T)] = component;
            
            return (T)component;
        }

        public bool TryGet<T>(out T component) where T : class
        {
            component = null;

            if (_componentsCache.TryGetValue(typeof(T), out object componentObject))
            {
                component = (T)componentObject;

                return true;
            }
            
            component = _context.Container.TryResolve<T>();

            return component != null;
        }
    }
}