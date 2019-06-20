using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_manager : MonoBehaviour
{
    public GameObject[] menus; // 0 = menú, 1 = settings
    public GameObject ingame; 

    public void ChangeScene(string scene)
    { 
        if(scene == "Gameplay" && PlayerPrefs.GetInt("Tutorial", 0) == 0) SceneManager.LoadScene("Cinematics");
        else SceneManager.LoadScene(scene);
    }

    public void OpenCloseSettings(bool opened)
    {
        if (opened)
        {
            menus[0].SetActive(true);
            menus[1].SetActive(false);
            opened = false;
        }
        else if (!opened)
        {
            menus[0].SetActive(false);
            menus[1].SetActive(true);
            opened = true;
        }
    }
    public void OpenCloseSettingsGame(bool opened)
    {
        if (opened)
        {
            ingame.SetActive(false);
            opened = false;
            GameObject.FindGameObjectWithTag("Character").GetComponent<Movement>().paused = false;
            GameObject.Find("Obstacles").GetComponent<ObstacleGenerator>().RestoreSpeed();
        }
        else if (!opened && !GameObject.FindGameObjectWithTag("Character").GetComponent<Movement>().shooting)
        {
            ingame.SetActive(true);
            opened = true;
            GameObject.FindGameObjectWithTag("Character").GetComponent<Movement>().paused = true;
        }
    }
    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
    }
}
