using System;
using System.Collections.Generic;
using System.Linq;

namespace Scripts.InteractableObjects
{
    /// <summary>
    /// Сервис локатор
    /// </summary>
    public class FactoriesProvider 
    {
        Dictionary<Type, object> _factories;

        public FactoriesProvider()
        {
            _factories = new Dictionary<Type, object>();
        }
        public void RegisterFabric<T, D>
            (IGameObjectFactory<T, D> interactableObjectFactory)
        {
            Type key = interactableObjectFactory.GetType();
            if(!_factories.ContainsKey(key))
            {
                _factories[key] = interactableObjectFactory;
            }
        }
        public object GetFactory<F>()
        {
            Type t = typeof(F);
            return (F)_factories[t];
        }
    }
}
