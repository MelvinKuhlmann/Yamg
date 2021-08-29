using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/**
 * Inspiration for code: https://pavcreations.com/scrollable-menu-in-unity-with-button-or-key-controller/
 */

public class ShopInventoryController : MonoBehaviour
{
    public bool keyDown;
    public RectTransform rectTransform;
    public List<Item> items = new List<Item>();
    public GameObject shopItemPrefab;
    public Item moneyItem;
    public InventoryController inventoryController;
    public UnityEvent<Item> OnItemSelected;

    [HideInInspector]
    public int index;
    bool isPressUp, isPressDown;
    int maxIndex;
    float itemHeight = 85f; // static height of inventory items, it's set in the prefab
    int VerticalMovement;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        isPressUp = isPressDown = false;
    }

    public void OnEnable()
    {
        maxIndex = items.Count - 1;

        int childIndex = 0;
        foreach (Item item in items)
        {
            if (shopItemPrefab != null)
            {
                GameObject gameObject = Instantiate(shopItemPrefab, Vector3.zero, Quaternion.identity, transform) as GameObject;
                gameObject.GetComponent<ShopItem>().controller = this;
                gameObject.GetComponent<ShopItem>().thisIndex = childIndex;
                gameObject.GetComponent<ShopItem>().item = item;
                gameObject.GetComponent<ShopItem>().UpdateState();
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

            foreach (Transform child in transform)
            {
                child.gameObject.GetComponent<ShopItem>().UpdateState();
            }
        }
    }
}