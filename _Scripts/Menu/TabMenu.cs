using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TabMenu : MonoBehaviour
{
    public GameObject CanvasMain;
    public GameObject Tab;
    public GameObject SettingsCanvas;
    public GameObject InventoryTab;
    public GameObject BuyMenu;

    [Header("Script")]

    public MouseLoop2 Mouse;
    public Movement movement;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Tab.activeSelf)
            {
                Continue();
                Cursor.visible = false;
                return;
            }
            if (SettingsCanvas.activeSelf)
            {                
                Continue();
                Cursor.visible = false;
                return;
            }
            Tab.SetActive(true);
            CanvasMain.SetActive(false);
            Cursor.lockState = CursorLockMode.None; 
            Cursor.visible = true;
            Mouse = GameObject.FindGameObjectWithTag("Camera2").GetComponent<MouseLoop2>();
            Mouse.tabMenu = true;

            movement.pause=true;
            Mouse.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Tab) && InventoryTab.activeSelf == false && Tab.activeSelf == false)
        {
            InventoryTab.SetActive(true);

        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            InventoryTab.SetActive(false);
        }
    }

    public void OpenLobby()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void OpenMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void Continue()
    {
        CanvasMain.SetActive(true);
        Tab.SetActive(false);
        SettingsCanvas.SetActive(false);

        Mouse = GameObject.FindGameObjectWithTag("Camera2").GetComponent<MouseLoop2>();
        Mouse.tabMenu = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;

        movement.pause = false;
        Mouse.enabled = true;
    }
    public void Settings()
    {
        SettingsCanvas.SetActive(true);
        Tab.SetActive(false);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }


}
