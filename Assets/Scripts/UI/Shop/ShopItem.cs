using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace YAMG
{
    public class ShopItem : MonoBehaviour
    {
        public ShopInventoryController controller;
        public GameObject selectedIcon;
        public int thisIndex;
        public Image icon, moneyIcon;
        public TMP_Text valueLabel;
        public Material materialItemLocked;
        public Material materialItemUnlocked;
        [HideInInspector]
        public Item item;

        void Update()
        {
            selectedIcon.SetActive(controller.index == thisIndex);
        }

        public void UpdateState()
        {
            icon.sprite = item.sprite;
            icon.material = materialItemUnlocked;
            moneyIcon.sprite = controller.moneyItem.sprite;
            moneyIcon.material = materialItemUnlocked;
            valueLabel.text = item.buyValue.ToString();
            if (controller.inventoryController.GetItemAmount(controller.moneyItem) < item.buyValue)
            {
                icon.material = materialItemLocked;
                moneyIcon.material = materialItemLocked;
            }
        }
    }
}