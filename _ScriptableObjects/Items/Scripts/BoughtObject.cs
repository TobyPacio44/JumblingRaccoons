using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bought Object", menuName = "Inventory/Items/Bought")]
public class BoughtObject : ItemObject
{
    public int price;
    public string title;
    public void Awake()
    {
        type = ItemType.Bought;
    }
}
