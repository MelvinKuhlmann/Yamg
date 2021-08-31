using UnityEngine;
using TMPro;
using System;

namespace YAMG
{
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
            if (item == null) throw new NotImplementedException("UI can not be updated, Item has not been set in the prefab");
            quantityLabel.SetText(controller.GetItemAmount(item).ToString());
        }
    }
}