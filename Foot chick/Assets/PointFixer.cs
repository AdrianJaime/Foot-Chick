using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointFixer : MonoBehaviour
{
    public int points, speed;
    private int record;
    public GameObject addedPoints;
    private bool grown = false, effect = false;
    // Start is called before the first frame update
    void Start()
    {
        points = 0;
        GetComponent<TMPro.TextMeshProUGUI>().text = points.ToString();
        record = PlayerPrefs.GetInt("Record", 0);
    }

    private void Update()
    {
        if (effect)
        {
            if (!grown)
            {
                addedPoints.GetComponent<TMPro.TextMeshProUGUI>().fontSize += Time.deltaTime * speed;
                if (addedPoints.GetComponent<TMPro.TextMeshProUGUI>().fontSize >= 120) grown = true;
            }
            else
            {
                addedPoints.GetComponent<TMPro.TextMeshProUGUI>().fontSize -= Time.deltaTime * speed;
                if (addedPoints.GetComponent<TMPro.TextMeshProUGUI>().fontSize <= 50)
                {
                    effect = grown = false;
                    addedPoints.GetComponent<TMPro.TextMeshProUGUI>().text = "";

                }
            }
        }
    }
    public void AddPoints(int _points)
    {
        points += _points;
        if(_points > 1)
        {
            addedPoints.GetComponent<TMPro.TextMeshProUGUI>().text = "+" + _points;
            effect = true;
        }
    }
    public void UpdatePoints()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = points.ToString();
    }


}
