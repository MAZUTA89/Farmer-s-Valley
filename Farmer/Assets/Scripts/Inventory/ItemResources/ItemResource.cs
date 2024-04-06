using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.InventoryCode
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class ItemResource : MonoBehaviour
    {
        Rigidbody2D _rb;
        SpriteRenderer _sr;
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _sr = GetComponent<SpriteRenderer>();
        }

        public void Initialize(Sprite sprite)
        {
            _sr.sprite = sprite;
        }
    }
}
