using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject player;


    //disable them in list like hats
    public GameObject main;
    public GameObject shop;
    public GameObject shop2;
    public GameObject shopSkins;
    public GameObject settings;

    public void OpenMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void OpenLevel()
    {
        SceneManager.LoadScene(1);      
    }
    public void ExitGame()
    {
        Application.Quit();
    }
  
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void CamMenu()
    {
        main.SetActive(true);       
        shop.SetActive(false);
        shop2.SetActive(false);
        shopSkins.SetActive(false);
        settings.SetActive(false);

        player.transform.position = new Vector3(361.891846f, -53.0922356f, -439.447021f);
    }
    public void CamShop()
    {
        main.SetActive(false);
        shop.SetActive(true);
        shop2.SetActive(false);
        shopSkins.SetActive(false);

        player.transform.position = new Vector3(259.399994f, -53.0922356f, -458.700012f);
    }

    public void CamShop2()
    {      
        main.SetActive(false);
        shop.SetActive(false);
        shop2.SetActive(true);
        shopSkins.SetActive(false);

        player.transform.position = new Vector3(259.399994f, -53.0922356f, -458.700012f);
    }

    public void CamShopSkins()
    {
        main.SetActive(false);
        shop.SetActive(false);
        shop2.SetActive(false);
        shopSkins.SetActive(true);

        player.transform.position = new Vector3(180.89183f, -33.0922356f, -443.447021f);

    }
    public void Settings()
    {
        main.SetActive(false);
        settings.SetActive(true);
    }
}


