using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swipes : MonoBehaviour
{

    public Sprite[] swipeSprites;
    public GameObject swipe;
    public GameObject[] swipeArr;
    private Movement charData;
    RectTransform swipeRect, containerRect;
    float width, ratio, height;
    ObstacleGenerator obsData;
    // Start is called before the first frame update
    void Start()
    {
        containerRect = GetComponent<RectTransform>();
        swipeRect = swipe.GetComponent<RectTransform>();
        obsData = GameObject.Find("Obstacles").GetComponent<ObstacleGenerator>();
        charData = GameObject.FindGameObjectWithTag("Character").GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSwipes(bool swiped)
    {
        swipeArr = GameObject.FindGameObjectsWithTag("Swipe");
        for (int i = 0; i < charData.swipeList.Count; i++)
        {
            swipeArr[i].GetComponent<Image>().sprite = swipeSprites[charData.swipeList[i]];
            if (!swiped) swipeArr[i].transform.localScale = new Vector3(1, 1, 1);
        }
        if (charData.swipeList.Count >= 0 && swiped)
        {
            swipeArr[charData.swipeList.Count].transform.localScale = new Vector3(0, 1, 1);
            swiped = false;
        }
    }
}
