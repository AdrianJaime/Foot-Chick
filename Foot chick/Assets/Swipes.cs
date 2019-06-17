using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Swipes : MonoBehaviour
{

    public Sprite[] swipeSprites;
    public GameObject swipe;
    public GameObject[] swipeArr;
    private Movement charData;
    ObstacleGenerator obsData;
    // Start is called before the first frame update
    void Start()
    {
        obsData = GameObject.Find("Obstacles").GetComponent<ObstacleGenerator>();
        charData = GameObject.FindGameObjectWithTag("Character").GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSwipes(bool swiped)
    {
        swipeArr = GameObject.FindGameObjectsWithTag("Swipe").OrderBy(GameObject => GameObject.name).ToArray();
        for (int i = 0; i < charData.swipeList.Count; i++)
        {
            swipeArr[i].GetComponent<Image>().sprite = swipeSprites[charData.swipeList[i]];
            if (!swiped) swipeArr[i].transform.localScale = new Vector3(1, 1, 1);
            //Change alpha
            if (i > 0 && PlayerPrefs.GetInt("Tutorial", 0) == 0)
            {
                swipeArr[i].GetComponent<Image>().color = new Color(swipeArr[i].GetComponent<Image>().color.r, swipeArr[i].GetComponent<Image>().color.g, swipeArr[i].GetComponent<Image>().color.b, 0.5f);
            }
        }
        if (charData.swipeList.Count >= 0 && swiped)
        {
            if (PlayerPrefs.GetInt("Tutorial", 0) == 0)
            {
                swipeArr[charData.swipeList.Count].GetComponent<Image>().color = new Color(swipeArr[charData.swipeList.Count].GetComponent<Image>().color.r, swipeArr[charData.swipeList.Count].GetComponent<Image>().color.g, swipeArr[charData.swipeList.Count].GetComponent<Image>().color.b, 1);
            }
            swipeArr[charData.swipeList.Count].transform.localScale = new Vector3(0, 1, 1);
            swiped = false;
        }
    }
}
