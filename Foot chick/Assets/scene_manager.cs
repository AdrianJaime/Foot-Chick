using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_manager : MonoBehaviour
{
    public GameObject[] menus; // 0 = menú, 1 = settings

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
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
    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
    }
}
