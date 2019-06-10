using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public int lifes;
    public Sprite lostLife, fullLife;
    // Start is called before the first frame update
    void Start()
    {
        lifes = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifes == 0)
        {
            Button[] ButtonList = Resources.FindObjectsOfTypeAll<Button>();
            foreach (Button button in ButtonList)
            {
                if (button.name == "Restart") button.gameObject.SetActive(true);

            }
            GameObject.Find("Obstacles").GetComponent<ObstacleGenerator>().gameStarted = false;
            GameObject.FindGameObjectWithTag("Character").GetComponent<Transform>().position = Vector3.zero;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().position = new Vector3(0, 0, GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().position.z);
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Obstacle").Length; i++) Destroy(GameObject.FindGameObjectsWithTag("Obstacle")[i]);
        }
    }
    public void LoseLife()
    {
        GameObject.Find("Heart " + lifes).GetComponent<Image>().sprite = lostLife;
        lifes--;
             
    }
    public void Restart()
    {
        lifes = 3;
        for (int i = 1; i < 4; i++) GameObject.Find("Heart " + i).GetComponent<Image>().sprite = fullLife;
        GameObject.Find("Obstacles").GetComponent<ObstacleGenerator>().speed = 5;
        GameObject.Find("Restart").SetActive(false);
    }
}
