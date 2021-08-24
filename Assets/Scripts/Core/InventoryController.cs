using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        public Dictionary<int, int> inventoryItems;
        public UnityEvent OnHasItem, OnDoesNotHaveItem;

        public bool CheckInventory(InventoryController inventory)
        {
            if (inventory != null)
            {
                foreach (KeyValuePair<int, int> kvp in inventoryItems)
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

    Dictionary<int, int> m_InventoryItems = new Dictionary<int, int>();

    //Debug function useful in editor during play mode to print in console all objects in that InventoyController
    [ContextMenu("Dump")]
    void Dump()
    {
        if (m_InventoryItems.Count == 0)
        {
            Debug.Log("Inventory is empty");
        }
        foreach (KeyValuePair<int, int> kvp in m_InventoryItems)
        {
            Debug.Log(kvp.Key + " : " + kvp.Value);
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

    public void AddItem(int key, int amount)
    {
        if (!m_InventoryItems.ContainsKey(key))
        {
            m_InventoryItems.Add(key, amount);
        }
        else
        {
            m_InventoryItems[key] += amount;
        }
        var ev = GetInventoryEvent(key);
        if (ev != null) ev.OnAdd.Invoke(amount);
    }

    public void RemoveItem(int key)
    {
        if (m_InventoryItems.ContainsKey(key))
        {
            var ev = GetInventoryEvent(key);
            if (ev != null) 
            { 
                ev.OnRemove.Invoke(); 
            }
            m_InventoryItems.Remove(key);
        }
    }

    public void SubtractItem(int key, int amount)
    {
        if (m_InventoryItems.TryGetValue(key, out int val))
        {
            if (val < amount)
            {
                return;
            }
            m_InventoryItems[key] -= amount;
            var ev = GetInventoryEvent(key);
            if (ev != null) ev.OnSubtract.Invoke(amount);
        }
    }

    public bool HasItem(int key)
    {
        return m_InventoryItems.ContainsKey(key);
    }

    public void Clear()
    {
        m_InventoryItems.Clear();
    }

    InventoryEvent GetInventoryEvent(int key)
    {
        foreach (var iv in inventoryEvents)
        {
            if (iv.item.id == key) return iv;
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
        return new Data<Dictionary<int, int>>(m_InventoryItems);
    }

    public void LoadData(Data data)
    {
        Data<Dictionary<int, int>> inventoryData = (Data<Dictionary<int, int>>)data;
        foreach (KeyValuePair<int, int> kvp in inventoryData.value)
        {
            AddItem(kvp.Key, kvp.Value);
            var ev = GetInventoryEvent(kvp.Key);
            if (ev != null) ev.OnSet.Invoke(kvp.Value);
        }
        if (OnInventoryLoaded != null) OnInventoryLoaded();
    }
}
