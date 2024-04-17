using Scripts.InteractableObjects;
using Scripts.SaveLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Scripts.InteractableObjects
{
    public interface IInteractableObjectFactory<out T, in D>
        where T : IInteractable
    {
        T Create(D createData);
    }
}
