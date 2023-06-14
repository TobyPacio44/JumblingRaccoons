using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipTime : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    [HideInInspector] private DayNightScript DayNight;
    [HideInInspector] public float animationDuration = 24f;
    [HideInInspector] public float currentTime;
    public int hoursToSkip;

    private void Start()
    {
        animator = GetComponent<Animator>();
        DayNight = GetComponent<DayNightScript>();  
    }
    private void Update()
    {
        currentTime = (DayNight.animationTime - 6 )  / animationDuration;
        if (Input.GetKeyDown(KeyCode.T)) 
        {            
            SkipHours(currentTime, hoursToSkip);
        }
    }

    // I have no idea why it skips exactly 6 hours
    public void SkipHours(float currentTime, int hoursToSkip)
    {
        animator.Play("DayNight", -1, currentTime + hoursToSkip/animationDuration);
    }

}
