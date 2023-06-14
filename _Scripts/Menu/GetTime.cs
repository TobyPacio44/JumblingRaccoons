using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetTime : MonoBehaviour
{
    public DayNightScript time;
    public TextMeshProUGUI hour;
    public TextMeshProUGUI clock;

    public void Start()
    {
        time = GameObject.FindWithTag("Time").GetComponent<DayNightScript>();
        hour = transform.Find("hour").GetComponent<TextMeshProUGUI>();
        clock = transform.Find("clock").GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        hour.text = time.time.ToString();
        clock.text = time.clockTime.ToString();
    }
}
