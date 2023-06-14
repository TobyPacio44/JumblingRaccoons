using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyBuilding : MonoBehaviour
{
    public BoughtObject buildingData; // A reference to the scriptable object that contains building data
    public Transform spawnPoint; // The point at which to spawn the building
    public GameObject purchasePrompt; // A UI element to display when the player is in the trigger zone
    public int playerPoints;

    public bool canPurchase = false; // Whether the player is in the trigger zone and able to purchase the building

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject parent = other.transform.parent.gameObject;
            playerPoints = parent.GetComponentInChildren<Points>().points;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerPoints >= buildingData.price)
            {
                canPurchase = true;
                return;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPurchase = false;
            purchasePrompt.SetActive(false);
            playerPoints = 0;
            purchasePrompt = null;
        }
    }

    private void Update()
    {
        if (canPurchase && Input.GetKeyDown(KeyCode.E))
        {
            playerPoints -= buildingData.price;
            PlayerPrefs.SetInt("points", playerPoints);
            PlayerPrefs.Save();

            PurchaseBuilding();
        }
    }

    private void PurchaseBuilding()
    {
        // Spawn the building at the spawn point
        Instantiate(buildingData.prefab, spawnPoint.position, spawnPoint.rotation);

        // Add any necessary components or logic to the building GameObject here

        // Destroy the trigger and purchase prompt UI element
        Destroy(gameObject);
        purchasePrompt.SetActive(false);
        purchasePrompt = null;
    }

    public void SetMenuText(GameObject ShopMenu)
    {
        BuyMenu menu;
        menu = ShopMenu.GetComponent<BuyMenu>();
        menu.title.text = buildingData.title;
        menu.price.text = buildingData.price.ToString();
        menu.description.text = buildingData.description;
    }

}
