using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallFixer : MonoBehaviour
{
    public int points;
    // Start is called before the first frame update
    void Start()
    {
        points = PlayerPrefs.GetInt("Balls", 0);
        GetComponent<TMPro.TextMeshProUGUI>().text = points.ToString();
    }
    public void SumPoints()
    {
        points++;
        GetComponent<TMPro.TextMeshProUGUI>().text = points.ToString();
    }
}
