using System;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.NextLevel
{
    public class NextLevelTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject _nextLevelPanel;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Player")
            {
                _nextLevelPanel.SetActive(true);
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                _nextLevelPanel.SetActive(false);
            }
        }
    }
}
