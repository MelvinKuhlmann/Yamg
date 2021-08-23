using UnityEngine;

public class MoneyChest : MonoBehaviour, IDataPersister
{
    public GameObject incompletedState;
    public GameObject completedState;
    public DataSettings dataSettings;

    void OnEnable()
    {
        PersistentDataManager.RegisterPersister(this);
    }

    void OnDisable()
    {
        PersistentDataManager.UnregisterPersister(this);
    }

    public void Save()
    {
        PersistentDataManager.SetDirty(this);
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
        bool isCompleted = !incompletedState.activeInHierarchy && completedState.activeInHierarchy;
        return new Data<bool>(isCompleted);
    }

    public void LoadData(Data data)
    {
        Data<bool> isCompleted = (Data<bool>)data;
        incompletedState.SetActive(!isCompleted.value);
        completedState.SetActive(isCompleted.value);
    }
}
