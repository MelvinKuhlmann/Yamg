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
    [SerializeField] GameObject menuItemPrefab;
    [SerializeField] UnityEvent<Item> OnItemSelected, OnItemBuy;

    [HideInInspector]
    public int index;
    [HideInInspector]
    public bool isPressUp, isPressDown, isPressConfirm;

    int maxIndex;
    float itemHeight = 64f; // static height of inventory items, it's set in the prefab
    int VerticalMovement;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        isPressUp = isPressDown = false;
    }

    public void OnEnable()
    {
        Dictionary<Item, int> inventory = new Dictionary<Item, int>();

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
            if (menuItemPrefab != null)
            {
                GameObject gameObject = Instantiate(menuItemPrefab, Vector3.zero, Quaternion.identity, transform) as GameObject;
                gameObject.GetComponent<ShopItem>().controller = this;
                gameObject.GetComponent<ShopItem>().thisIndex = childIndex;
                gameObject.GetComponent<ShopItem>().animator = gameObject.GetComponent<Animator>();
                gameObject.transform.GetChild(1).GetComponent<Image>().sprite = entry.Key.sprite;
                gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = entry.Key.buyValue.ToString();
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

        if (PlayerInput.Instance.Buy.Down)
        {
            OnItemBuy.Invoke(items[index]);
        }
    }

    public void onPressUp()
    {
        isPressUp = true;
    }

    public void onReleaseUp()
    {
        isPressUp = false;
    }

    public void onPressDown()
    {
        isPressDown = true;
    }

    public void onReleaseDown()
    {
        isPressDown = false;
    }

    public void onPressConfirm()
    {
        isPressConfirm = true;
    }

    public void onReleaseConfirm()
    {
        isPressConfirm = false;
    }
}