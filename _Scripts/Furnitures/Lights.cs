using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    public GameObject Light;
    public bool LightsOn = true;

    private void Start()
    {
        LightOn();
    }
    public void LightOn()
    {
        if (LightsOn)
        { Light.SetActive(true); }
        else
            { Light.SetActive(false); }
    }
}
