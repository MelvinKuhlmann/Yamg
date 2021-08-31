using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace YAMG
{
    public class InventoryController : MonoBehaviour, IDataPersister
    {
        [System.Serializable]
        public class InventoryEvent
        {
            public Item item;
            public UnityEvent<int> OnAdd, OnSet, OnSubtract;
            public UnityEvent OnRemove;
        }

        [System.Serializable]
        public class InventoryChecker
        {
            public Dictionary<Item, int> inventoryItems;
            public UnityEvent OnHasItem, OnDoesNotHaveItem;

            public bool CheckInventory(InventoryController inventory)
            {
                if (inventory != null)
                {
                    foreach (KeyValuePair<Item, int> kvp in inventoryItems)
                    {
                        if (!inventory.HasItem(kvp.Key))
                        {
                            OnDoesNotHaveItem.Invoke();
                            return false;
                        }
                    }
                    OnHasItem.Invoke();
                    return true;
                }
                return false;
            }
        }

        public InventoryEvent[] inventoryEvents;
        public event System.Action OnInventoryLoaded;

        public DataSettings dataSettings;

        Dictionary<Item, int> m_InventoryItems = new Dictionary<Item, int>();

        //Debug function useful in editor during play mode to print in console all objects in that InventoyController
        [ContextMenu("Dump")]
        void Dump()
        {
            if (m_InventoryItems.Count == 0)
            {
                Debug.Log("Inventory is empty");
            }
            foreach (KeyValuePair<Item, int> kvp in m_InventoryItems)
            {
                Debug.Log(kvp.Key.itemName + " : " + kvp.Value);
            }
        }

        void OnEnable()
        {
            PersistentDataManager.RegisterPersister(this);
        }

        void OnDisable()
        {
            PersistentDataManager.UnregisterPersister(this);
        }

        public void AddItem(Item item, int amount)
        {
            if (!m_InventoryItems.ContainsKey(item))
            {
                m_InventoryItems.Add(item, amount);
            }
            else
            {
                m_InventoryItems[item] += amount;
            }
            var ev = GetInventoryEvent(item);
            if (ev != null) ev.OnAdd.Invoke(amount);
        }

        public void RemoveItem(Item item)
        {
            if (m_InventoryItems.ContainsKey(item))
            {
                m_InventoryItems.Remove(item);
                var ev = GetInventoryEvent(item);
                if (ev != null) ev.OnRemove.Invoke();
            }
        }

        public void SubtractItem(Item item, int amount)
        {
            if (m_InventoryItems.TryGetValue(item, out int val))
            {
                if (val < amount)
                {
                    return;
                }
                m_InventoryItems[item] -= amount;
                var ev = GetInventoryEvent(item);
                if (ev != null) ev.OnSubtract.Invoke(amount);
            }
        }

        public bool HasItem(Item item)
        {
            return m_InventoryItems.ContainsKey(item);
        }

        public int GetItemAmount(Item item)
        {
            if (m_InventoryItems.TryGetValue(item, out int val))
            {
                return val;
            }
            return 0;
        }

        public void Clear()
        {
            m_InventoryItems.Clear();
        }

        InventoryEvent GetInventoryEvent(Item item)
        {
            foreach (var iv in inventoryEvents)
            {
                if (iv.item.id == item.id) return iv;
            }
            return null;
        }

        public DataSettings GetDataSettings()
        {
            return dataSettings;
        }

        public void SetDataSettings(string dataTag, DataSettings.PersistenceType persistenceType)
        {
            dataSettings.dataTag = dataTag;
            dataSettings.persistenceType = persistenceType;
        }

        public Data SaveData()
        {
            return new Data<Dictionary<Item, int>>(m_InventoryItems);
        }

        public void LoadData(Data data)
        {
            Data<Dictionary<Item, int>> inventoryData = (Data<Dictionary<Item, int>>)data;
            foreach (KeyValuePair<Item, int> kvp in inventoryData.value)
            {
                AddItem(kvp.Key, kvp.Value);
                var ev = GetInventoryEvent(kvp.Key);
                if (ev != null) ev.OnSet.Invoke(kvp.Value);
            }
            if (OnInventoryLoaded != null) OnInventoryLoaded();
        }
    }
}