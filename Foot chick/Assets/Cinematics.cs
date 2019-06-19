using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematics : MonoBehaviour
{
    public GameObject[] text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Tutorial", 0) == 0 && !text[0].activeSelf) text[0].SetActive(true);
        else if(!text[1].activeSelf) text[1].SetActive(true);

    }
}
