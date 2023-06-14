using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Points : MonoBehaviour
{
    public int points;
    private void Awake()
    {
        points = PlayerPrefs.GetInt("points");
        GetComponent<TextMeshProUGUI>().text = points.ToString();
        StartCoroutine(RepeatFunction());
    }

    private IEnumerator RepeatFunction()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            points = PlayerPrefs.GetInt("points");
            GetComponent<TextMeshProUGUI>().text = points.ToString();
        }
    }
}

