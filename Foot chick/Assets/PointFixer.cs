using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointFixer : MonoBehaviour
{
    public int points;
    private int record;
    // Start is called before the first frame update
    void Start()
    {
        points = 0;
        GetComponent<TMPro.TextMeshProUGUI>().text = points.ToString();
        record = PlayerPrefs.GetInt("Record", 0);
    }

    public void UpdatePoints()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = points.ToString();
    }

}
