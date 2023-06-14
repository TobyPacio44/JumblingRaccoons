using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class FruitRandomSpawner : MonoBehaviour
{
    public Collider[] spawnerCollider;
    public Mission mission;
    public TextMeshProUGUI objective_list;
    public GameObject[] food;
    public GameObject[] positions;
    Transform[] positionsTransforms;
    Transform[] randomTransRam = new Transform[30];
    Transform t;

    public bool started = false;
    public int difficulty;
    int missionAdd = 0;

    public List<GameObject> points = new List<GameObject>();

    //GameObject Gary is a text dialog on Mission start

    void Start()
    {
        spawnerCollider = gameObject.GetComponents<Collider>();
        positionsTransforms = new Transform[positions.Length];

        for (int i = 0; i < positions.Length; i++)
        {
            positionsTransforms[i] = positions[i].transform;
        }

        for (int i = 0; i < difficulty; i++)
        {
            t = positionsTransforms[Random.Range(0, positions.Length)];
            if (!randomTransRam.Contains(t))
            {
                randomTransRam[i] = t;
            }
            else if (randomTransRam.Contains(t))
            {
                i--;
            }
        } 
        
    }    
    public void StartMission()
    {
        objective_list = mission.objective_list;
        started = true;

        for (int i = 0; i >= spawnerCollider.Length; i++)
        {
            spawnerCollider[i].enabled = false;
        }

        difficulty = mission.difficulty;
        for (int i = 0; i < difficulty; i++)
        {
            // prefeb = Random.Range(0, food.Length);

            GameObject newGO = Instantiate(food[Random.Range(0, food.Length)], randomTransRam[i].position, Quaternion.identity);
            newGO.name = newGO.name.Replace("(Clone)", "");
            points.Add(newGO);
            if (missionAdd < difficulty && !mission.mission.Contains(newGO))
            {
                mission.mission.Add(newGO);
                missionAdd++;
            }
        }

        ListSave();
    }
    
    public void ListSave()
    {
        objective_list.text = "";
        for (int i = 0; i < mission.mission.Count; i++)
        {
            objective_list.text += "- " + mission.mission[i].name + "<br>";
        }
    }


}
