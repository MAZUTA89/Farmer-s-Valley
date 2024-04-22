
using Scripts.SO.InteractableObjects;
using UnityEngine;

namespace Scripts.InteractableObjects
{
    public class Seed : MonoBehaviour, IInteractable
    {
        SeedSO _seedSO;
        public void Initialize(SeedSO seedSO)
        {
            _seedSO = seedSO;
        }
    }
}
