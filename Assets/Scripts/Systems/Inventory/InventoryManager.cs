using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject  inventoryMenu;
    private bool        menuActivated = false;

    void Update()
    {
        if (Input.GetButtonDown("Inventory") && !menuActivated)
        {
            inventoryMenu.SetActive(true);
            menuActivated = true;
        }
        else if (Input.GetButtonDown("Inventory") && menuActivated)
        {
            inventoryMenu.SetActive(false);
            menuActivated = false;
        }
    }
}
