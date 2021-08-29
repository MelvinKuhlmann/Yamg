using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ShopInventoryController : MonoBehaviour
{
    [SerializeField] bool keyDown;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] List<Item> items = new List<Item>();
    [SerializeField] GameObject shopItemPrefab;
    [SerializeField] Item moneyItem;
    [SerializeField] InventoryController inventoryController;
    [SerializeField] UnityEvent<Item> OnItemSelected;

    [HideInInspector]
    public int index;
    [HideInInspector]
    public bool isPressUp, isPressDown, isPressConfirm;

    int maxIndex;
    float itemHeight = 64f; // static height of inventory items, it's set in the prefab
    int VerticalMovement;
    Dictionary<Item, int> inventory = new Dictionary<Item, int>();

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        isPressUp = isPressDown = false;
    }

    public void OnEnable()
    {
        inventory.Clear();
        foreach (Item item in items)
        {
            if (inventory.ContainsKey(item))
            {
                inventory[item] += 1;
            }
            else
            {
                inventory.Add(item, 1);
            }
        }

        maxIndex = inventory.Count - 1;

        int childIndex = 0;
        foreach (KeyValuePair<Item, int> entry in inventory)
        {
            if (shopItemPrefab != null)
            {
                GameObject gameObject = Instantiate(shopItemPrefab, Vector3.zero, Quaternion.identity, transform) as GameObject;
                gameObject.GetComponent<ShopItem>().controller = this;
                gameObject.GetComponent<ShopItem>().thisIndex = childIndex;
                gameObject.GetComponent<ShopItem>().animator = gameObject.GetComponent<Animator>();
                gameObject.transform.GetChild(1).GetComponent<Image>().sprite = entry.Key.sprite;
                gameObject.transform.GetChild(2).GetComponent<Image>().sprite = moneyItem.sprite;
                gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = entry.Key.buyValue.ToString();
                if (inventoryController.GetItemAmount(moneyItem) < entry.Key.buyValue)
                {
                    gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "N" + entry.Key.buyValue; // TODO add some visual effect that player has not enough resources
                }
                childIndex++;
            }
        }
        // resets to the first item
        index = 0;
        rectTransform.offsetMax = Vector2.zero;
        OnItemSelected.Invoke(items[index]);
    }

    public void OnDisable()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    void Update()
    {
        if (isPressUp) VerticalMovement = 1;
        if (isPressDown) VerticalMovement = -1;
        if (!isPressUp && !isPressDown) VerticalMovement = 0;

        if (PlayerInput.Instance.NavigationVertical.ReceivingInput || VerticalMovement != 0)
        {
            if (!keyDown)
            {
                if (PlayerInput.Instance.NavigationVertical.Value < 0 || VerticalMovement < 0)
                {
                    if (index < maxIndex)
                    {
                        index++;
                        if (index > 1 && index < maxIndex)
                        {
                            rectTransform.offsetMax -= new Vector2(0, (itemHeight * -1));
                        }
                    }
                    else
                    {
                        index = maxIndex;
                    }
                }

                else if (PlayerInput.Instance.NavigationVertical.Value > 0 || VerticalMovement > 0)
                {
                    if (index > 0)
                    {
                        index--;
                        if (index < maxIndex - 1 && index > 0)
                        {
                            rectTransform.offsetMax -= new Vector2(0, itemHeight);
                        }
                    }
                    else
                    {
                        index = 0;
                    }
                }
                keyDown = true;
            }
            OnItemSelected.Invoke(items[index]);
        }
        else
        {
            keyDown = false;
        }

        if (PlayerInput.Instance.Buy.Down && (inventoryController.GetItemAmount(moneyItem) >= items[index].buyValue)) //TODO make some more robust check
        {
            inventoryController.SubtractItem(moneyItem, items[index].buyValue);
            inventoryController.AddItem(items[index], 1);

           /* if (inventory.TryGetValue(items[index], out int val))
            {
                if (val < 1)
                {
                    return;
                }
                inventory[items[index]] -= 1;
            }*/
        }
    }
}