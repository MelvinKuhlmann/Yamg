using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public ShopInventoryController controller;
    public GameObject selectedIcon;
    public Animator animator;
    public int thisIndex;
    [SerializeField] GameObject menuPanelToOpen;

    void Update()
    {
        if (controller.index == thisIndex)
        {
            selectedIcon.SetActive(true);
            // animator.SetBool("selected", true);
            if (controller.isPressConfirm)
            {
                //  animator.SetBool("pressed", true);
                if (menuPanelToOpen != null)
                {

                    controller.gameObject.SetActive(false);
                    menuPanelToOpen.SetActive(true);
                }
            }
            /*  else if (animator.GetBool("pressed"))
              {
                  animator.SetBool("pressed", false);
              }*/
        }
        else
        {
            selectedIcon.SetActive(false);
            //  animator.SetBool("selected", false);
        }
    }
}
