using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeSkins : MonoBehaviour
{
    public ShopHats hats;
    public SkinChange skin;
    [SerializeField] int equippedHat;
    [SerializeField] int equippedFace;
    [SerializeField] int skinId;

    private void Awake()
    {        
        equippedHat = PlayerPrefs.GetInt("hat");
        equippedFace = PlayerPrefs.GetInt("face");

        if (equippedHat >= 0 ) { hats.hats[equippedHat].SetActive(true); }
        if (equippedFace >= 0) { hats.eyes[equippedFace].SetActive(true); }

    }
    private void Start()
    {
        skin.UpdateSkin();
    }
}
