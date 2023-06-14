using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopHats : MonoBehaviour
{
    public GameObject[] hats;
    public GameObject[] eyes;

    public int equippedHat;
    public int equippedFace;

    public void ClearHats()
    {
        for (int i = 0; i < hats.Length; i++)
        {
            hats[i].SetActive(false);
        }
        equippedHat = -1;
    }
    public void ClearEyes()
    {
        for (int i = 0; i < eyes.Length; i++)
        {
            eyes[i].SetActive(false);
        }
        equippedFace = -1;
    }

    public void equipCowboyHat()
    {
        ClearHats();
        hats[0].SetActive(true);
        equippedHat = 0;
    }
    public void equipMagicianHat()
    {
        ClearHats();
        hats[1].SetActive(true);
        equippedHat = 1;
    }
    public void equipSombrero()
    {
        ClearHats();
        hats[2].SetActive(true);
        equippedHat = 2;
    }
    public void equipMinerHat()
    {
        ClearHats();
        hats[3].SetActive(true);
        equippedHat = 3;
    }
    public void equipCrown()
    {
        ClearHats();
        hats[4].SetActive(true);
        equippedHat = 4;
    }
    public void equipPoliceCap()
    {
        ClearHats();
        hats[5].SetActive(true);
        equippedHat = 5;
    }

    public void equipBlackGlasses()
    {
        ClearEyes();
        eyes[0].SetActive(true);
        equippedFace = 0;
    }

}
