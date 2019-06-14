using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLevel : MonoBehaviour
{
    public Sprite[] musicSprites, soundSprites;
    public int musicLevel, soundLevel;

    public void Start()
    {
        soundLevel = PlayerPrefs.GetInt("SoundLevel", 0);
        musicLevel = PlayerPrefs.GetInt("MusicLevel", 0);
        GameObject.Find("MusicVol").GetComponent<Image>().sprite = musicSprites[musicLevel];
        GameObject.Find("SoundVol").GetComponent<Image>().sprite = soundSprites[soundLevel];
    }
    public void musicUp()
    {
        if (musicLevel == 3) musicLevel = -1;
        musicLevel++;
        GameObject.Find("MusicVol").GetComponent<Image>().sprite = musicSprites[musicLevel];
        PlayerPrefs.SetInt("MusicLevel", musicLevel);
    }

    public void soundUp()
    {
        if (soundLevel == 3) soundLevel = -1;
        soundLevel++;
        GameObject.Find("SoundVol").GetComponent<Image>().sprite = soundSprites[soundLevel];
        PlayerPrefs.SetInt("SoundLevel", soundLevel);
    }

    
}
