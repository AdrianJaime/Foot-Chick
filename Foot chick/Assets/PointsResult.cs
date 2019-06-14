using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsResult : MonoBehaviour
{
    public GameObject points;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void ResultPoints()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = points.GetComponent<PointFixer>().points.ToString();
    }
}
