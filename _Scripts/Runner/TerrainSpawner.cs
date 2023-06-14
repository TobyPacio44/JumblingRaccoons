using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawner : MonoBehaviour
{
    public GameObject terrain;
    public float spawnTime = 1;
    private float timer = 0;


    void Update()
    {
        if (timer > spawnTime)
        {           
            GameObject obs = Instantiate(terrain);
            obs.transform.position = transform.position + new Vector3(0, 0, 0);
            Destroy(obs, 25);
            timer = 0;
        }


        timer += Time.deltaTime;
    }

}
