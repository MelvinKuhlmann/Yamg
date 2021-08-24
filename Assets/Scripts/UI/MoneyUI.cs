using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    public static MoneyUI Instance { get; protected set; }

    public TMP_Text quantityLabel;
    public Item item;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetInitialKeyCount();
    }

    public void SetInitialKeyCount()
    {
        quantityLabel.SetText("0");
    }

    public void UpdateMoneyUI(InventoryController controller)
    {
        Debug.Log("Money added");
        if(item != null)
        {
            quantityLabel.SetText(controller.GetItemAmount(item).ToString());
        }
    }
}
