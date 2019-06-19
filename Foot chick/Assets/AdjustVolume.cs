using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustVolume : MonoBehaviour
{
    public float volumeFraction;
    int volumeLevel;
    public string wichLevel;
    // Start is called before the first frame update
    void Start()
    {
        volumeFraction = volumeFraction / 3; //Coloco el volumen en volumeFraction y me lo divide a los tres cachos que podría coger
    }

    // Update is called once per frame
    void Update()
    {
        volumeLevel = PlayerPrefs.GetInt(wichLevel, 1);
        switch (volumeLevel){
            case 0:
                GetComponent<AudioSource>().volume = 0;
                break;
            case 1:
                GetComponent<AudioSource>().volume = volumeFraction;
                break;
            case 2:
                GetComponent<AudioSource>().volume = volumeFraction * 2;
                break;
            case 3:
                GetComponent<AudioSource>().volume = volumeFraction * 3;
                break;
        }
    }
}
