using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swipes : MonoBehaviour
{

    public Sprite[] swipes;
    public GameObject swipe;
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

    public void setSwipes()
    {
        width = containerRect.rect.width / obsData.difficulty;
        ratio = width / swipeRect.rect.width;
        height = swipeRect.rect.height * ratio;
        for(int i = 0; i < charData.swipeList.Count; i++)
        {
            GameObject newItem = Instantiate(swipe, gameObject.transform);
            RectTransform itemRect = newItem.GetComponent<RectTransform>();
            float x = -containerRect.rect.width / 2 + width * i;
            float y = containerRect.rect.height / 2;
            x = itemRect.offsetMin.x + width;
            y = itemRect.offsetMin.y + height;
            itemRect.offsetMax = new Vector2(x, y);
        }
    }
}
