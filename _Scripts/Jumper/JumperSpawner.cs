using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JumperSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    public float spawnTime = 1;
    private float timer = 0;
    private float points;

    public Points PointAmount;

    [SerializeField] TextMeshProUGUI score_points;

    private void Start()
    {
        PointAmount = GameObject.FindGameObjectWithTag("PointsAmount").GetComponent<Points>();
        score_points.text = PointAmount.points.ToString();
    }
    void Update()
    {
        float y = Random.Range(0, 4);

        if (timer > spawnTime)
        {
            int rand = Random.Range(0, obstaclePrefab.Length);

            GameObject obs = Instantiate(obstaclePrefab[rand]);
            obs.transform.position = transform.position + new Vector3(0, y, 0);
            Destroy(obs, 15);
            timer = 0;
            
            PointAmount.points += 20;
            
            score_points.text = PointAmount.points.ToString();
        }


        timer += Time.deltaTime;
    }

}
