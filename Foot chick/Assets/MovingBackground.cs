using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackground : MonoBehaviour
{
    bool gameStarted, right = false;
    public float smooth, dist;
    public Camera cam;
    public Sprite[] backgrounds;
    int level = 1;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.Find("Obstacles").GetComponent<ObstacleGenerator>().gameEnded)
        {
            if (GameObject.Find("Points").GetComponent<PointFixer>().points >= level * 100)
            {
                level++;
                if (level == 4) GameObject.Find("SceneManager").GetComponent<scene_manager>().ChangeScene("Cinematics");
                else GetComponent<SpriteRenderer>().sprite = backgrounds[level - 1];
                GameObject.Find("Obstacles").GetComponent<ObstacleGenerator>().speed = 5;
                GameObject.Find("Obstacles").GetComponent<ObstacleGenerator>().speedAum += 0.1f;
                
            }
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) gameStarted = true;
        }
        if (gameStarted)
        {
            if (right) gameObject.transform.position = Vector3.MoveTowards(transform.position, new Vector3(13, transform.position.y, transform.position.z), Time.deltaTime * smooth);
            else gameObject.transform.position = Vector3.MoveTowards(transform.position, new Vector3(-13, transform.position.y, transform.position.z), Time.deltaTime * smooth);
            if (transform.position.x == 13 || transform.position.x == -13)
            {
                ResetTravel();
            }
        }
        
            
    }

    void ResetTravel()
    {
        if (right) right = false;
        else right = true;
    }
}
