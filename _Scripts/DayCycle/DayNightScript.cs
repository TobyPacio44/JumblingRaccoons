using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DayNightScript : MonoBehaviour
{
    public int time;
    [HideInInspector] public int animationTime;

    public GameObject day;
    public GameObject night;
    public GameObject lights;
    private Transform[] lightswitches;
    private void Awake()
    {
        lightswitches = lights.transform.GetComponentsInChildren<Transform>(true).Where(t => t.name == "Lightswitch").ToArray();
    }
    public enum clock
    {
        AM,
        PM
    }
    public clock clockTime;

    public void Time(int time)
    {
        this.time = time;
        this.animationTime = time;
        if (this.time > 12) { this.time -= 12; clockTime = clock.PM; }
        else { clockTime = clock.AM; }

        if(this.time == 6 && clockTime == clock.AM) { day.SetActive(true); night.SetActive(false); SetLights(false);  }
        if(this.time == 11 && clockTime == clock.PM) { day.SetActive(false); night.SetActive(true); SetLights(true); }

    }

    private void SetLights(bool set)
    {
        if (set)
        {
            foreach(Transform lightswitch in lightswitches)
            {
                lightswitch.gameObject.SetActive(true);    
            }
        }
        else
        {
            foreach (Transform lightswitch in lightswitches)
            {
                lightswitch.gameObject.SetActive(false);
            }
        }
    }
}
