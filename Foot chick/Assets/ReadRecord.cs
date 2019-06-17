using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadRecord : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TMPro.TextMeshProUGUI>().text = PlayerPrefs.GetInt("Record", 0).ToString();
    }


}
