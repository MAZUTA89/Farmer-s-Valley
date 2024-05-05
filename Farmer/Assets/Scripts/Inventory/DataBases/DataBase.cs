using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.InventoryCode
{
    public interface IDataBaseItem
    {
        string UniqueName { get;}
    }
    public class DataBase<T> : ScriptableObject
        where T : class, IDataBaseItem
    {
        public List<T> Items;
        public Dictionary<string, T> _itemsDictionary;
        public void Init()
        {
            _itemsDictionary = new Dictionary<string, T>();

            foreach (var item in Items)
            {
                if(item != null)
                {
                    _itemsDictionary.TryAdd(item.UniqueName, item);
                }
            }
        }
        public T GetItemByName(string name)
        {
            if (_itemsDictionary.TryGetValue(name,
                out var item))
            {
                return item;
            }
            else
                return null;
        }
    }
}
