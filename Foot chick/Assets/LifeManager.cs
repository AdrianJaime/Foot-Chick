using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public int lifes;
    public Sprite lostLife, fullLife;
    public GameObject[] screen;
    public GameObject newRecord, pointsResult; 
    public InputField inputField;
    public bool godMode;
    // Start is called before the first frame update
    void Start()
    {
        lifes = 3;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void LoseLife()
    {
        if (!godMode)
        {
            GameObject.Find("Heart " + lifes).GetComponent<Image>().sprite = lostLife;
            lifes--;
            if (lifes == 0) Die();
        }
    }


    private void SaveData()
    {
        if (PlayerPrefs.GetInt("Record", 0) < GameObject.Find("Points").GetComponent<PointFixer>().points)
        {
            PlayerPrefs.SetInt("Record", GameObject.Find("Points").GetComponent<PointFixer>().points);
            newRecord.SetActive(true);
        }
    }

    private void Die()
    {
        SaveData();
        Button[] ButtonList = Resources.FindObjectsOfTypeAll<Button>();
        GameObject.Find("Obstacles").GetComponent<ObstacleGenerator>().gameStarted = false;
        GameObject.Find("Obstacles").GetComponent<ObstacleGenerator>().gameEnded = true;
        GameObject.Find("Obstacles").GetComponent<ObstacleGenerator>().lastObject = null;
        GameObject.FindGameObjectWithTag("Character").GetComponent<Transform>().position = Vector3.zero;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().position = new Vector3(0, 0, GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().position.z);
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Obstacle").Length; i++) Destroy(GameObject.FindGameObjectsWithTag("Obstacle")[i]);
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Ball").Length; i++) Destroy(GameObject.FindGameObjectsWithTag("Ball")[i]);
        screen[0].SetActive(false);
        screen[1].SetActive(true);
        GameObject.Find("ResultPoints").GetComponent<PointsResult>().ResultPoints();
    }
    public void activateGod()
    {
        string text = inputField.text;
        if (text == "god")
            godMode = true;
        if (text == "mortal")
            godMode = false;
    }
}
