using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Mission : MonoBehaviour
{
    public List<GameObject> mission = new List<GameObject>();
    public FruitRandomSpawner spawner;
    public Inventory inventory;
    public int difficulty;
    public GameObject MissionUV;
    public TextMeshProUGUI objective_list;

    void OnTriggerEnter(Collider other)
    {      
        if (other.tag == "mission")
        {
            MissionUV.SetActive(true);
            Debug.Log("collision with mission");
            spawner = other.GetComponent<FruitRandomSpawner>();
            difficulty = spawner.difficulty;
            spawner.mission = this;
            spawner.StartMission();
        }
;
    }

    
    /* public void checkMission()
    {        
     for(int i = 0 ; i<mission.Count ; i++)
     {
         if (inventory.allPickedUp.Contains(mission[i]))
            {
                mission.RemoveAt(i);
            }
     }
        spawner.ListSave();
    }
    */

}
