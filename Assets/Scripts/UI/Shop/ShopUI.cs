using UnityEngine;
using TMPro;

namespace YAMG
{
    public class ShopUI : MonoBehaviour
    {
        public TMP_Text itemName;
        public TMP_Text itemDescription;

        public void ShowItemDetails(Item item)
        {
            itemName.text = item.itemName;
            itemDescription.text = item.description;
        }
    }
}