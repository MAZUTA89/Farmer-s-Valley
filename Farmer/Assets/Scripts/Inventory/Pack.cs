using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
    public class Pack : MonoBehaviour
    {
        [SerializeField] GameObject InventoryObject;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
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
