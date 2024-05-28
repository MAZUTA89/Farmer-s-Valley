using UnityEngine;
using Zenject;

namespace Scripts.InventoryCode
{
    public class Pack : MonoBehaviour
    {
        [SerializeField] GameObject InventoryObject;

        InputService _inputService;

        [Inject]
        public void Construct(InputService inputService)
        {
            _inputService = inputService;
        }


        public void Update()
        {
            if (_inputService.IsOpenCloseBackPack())
            {
                if (InventoryObject.activeSelf)
                {
                    InventoryObject.SetActive(false);
                }
                else
                {
                    InventoryObject.SetActive(true);
                }
            }
        }
    }
}
