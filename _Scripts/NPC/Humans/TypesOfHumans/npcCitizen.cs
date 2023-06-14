using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static DayNightScript;

public class npcCitizen : MonoBehaviour
{
    public DayNightScript time;
    public HumanAI Ai;

    public void Start()
    {
        time = GameObject.FindWithTag("Time").GetComponent<DayNightScript>();
        Ai = GetComponent<HumanAI>();
    }

    private void Update()
    {
        if (time.time == 10 && time.clockTime == clock.PM) 
        {
            Ai.state = HumanAI.State.sleeping;
            Debug.Log("Sleep time");
        }

        if (time.time == 6 && time.clockTime == clock.AM)
        {
            Ai.wakeUp();
            Debug.Log("Wake up time");
        }
    }
}
