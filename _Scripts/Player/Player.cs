using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Raccoon")]
    public GameObject raccoon;

    [Header("Scripts")]
    public Movement movement;
    public Crosshair Crosshair;
    public MouseLoop2 Camera;
    public Mission Mission;
    public Animations animations;
    public Points Points;

    [Header("Objects")]
    public GameObject Rig;
    public GameObject ShopUI;    
    public InventoryObject inventory;
}
