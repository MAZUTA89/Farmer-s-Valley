using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Scripts.InteractableObjects
{
    /// <summary>
    /// Сервис локатор
    /// </summary>
    public class InteractableObjectsFactoryProvider 
    {
        Dictionary<Type, object> _factories;

        public InteractableObjectsFactoryProvider()
        {
            _factories = new Dictionary<Type, object>();
        }
        public void RegisterFabric<T, D>
            (IInteractableObjectFactory<T, D> interactableObjectFactory)
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
