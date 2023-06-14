using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinChange : MonoBehaviour
{
    public Renderer rend;
    public Material[] myMaterials;
    public Material[] skins;
    public int skinId;

    private void Awake()
    {
          
    }
    public void UpdateSkin()
    {
        skinId = PlayerPrefs.GetInt("skin");
        myMaterials[1] = skins[skinId];
        rend.sharedMaterials = myMaterials;
    }

     private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (skinId == skins.Length-1)
            {
                skinId = 0;
                UpdateSkin();
            }else
            skinId++;
            UpdateSkin();
        }
    } 
    void Start()
    {
        rend = GetComponent<Renderer>();
        myMaterials = rend.sharedMaterials;
        UpdateSkin();
    }

    public void ChangeSkinDown()
    {
        if (skinId == 0)
        {
            skinId = skins.Length -1;
        }
        else
        skinId--;

        myMaterials[1] = skins[skinId];
        rend.sharedMaterials = myMaterials;
    }

    public void ChangeSkinUp()
    {
        if (skinId == skins.Length - 1)
        {
            skinId = 0;
        }
        else
            skinId++;

        myMaterials[1] = skins[skinId];
        rend.sharedMaterials = myMaterials;
    }

}
