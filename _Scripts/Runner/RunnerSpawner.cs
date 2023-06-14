using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    public float spawnTime = 1;
    private float timer = 0;

    private void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            int rand = Random.Range(0, obstaclePrefab.Length);

            transform.position = transform.position + new Vector3(0, 0, 55);
            GameObject obs = Instantiate(obstaclePrefab[rand]);
            obs.transform.position = transform.position + new Vector3(0, 0, 0);
                      
        }
        
        
    }

    void Update()
    {
       if (timer > spawnTime)
        {
            int rand = Random.Range(0, obstaclePrefab.Length);
            
            GameObject obs = Instantiate(obstaclePrefab[rand]);
            obs.transform.position = transform.position + new Vector3(0, 0, 0);
            Destroy(obs, 15);
            timer = 0;
        }


        timer += Time.deltaTime;
    }

}
