﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject[] Obstacles, Items;
    public float speed, speedAum, start_time;
    private float savedSpeed;
    public Camera cam;
    public bool gameStarted, gameEnded;
    public GameObject lastObject;

    public int difficulty; //number of swipes per ball

    public Vector2 lateralSize, verticalSize;

    // Start is called before the first frame update
    void Start()
    {
        if (difficulty < 4) difficulty = 4;
        gameEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            if (!GameObject.FindGameObjectWithTag("Character").GetComponent<Movement>().shooting && !GameObject.FindGameObjectWithTag("Character").GetComponent<Movement>().paused) speed += Time.deltaTime * speedAum;
            else if(savedSpeed == 0){
                savedSpeed = speed;
                speed = 0;
                    }
            if (lastObject.transform.position.z < 5)
            {
                lastObject = Instantiate(Obstacles[randomObstacle()], gameObject.transform);
                if (SummonItem() == 1) Instantiate(Items[0], gameObject.transform);
            }
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began /*&& !GameObject.Find("Restart")*/ && !gameStarted)
            {
                start_time = Time.time;
                if (!gameEnded)
                {
                    gameStarted = true;
                    lastObject = Instantiate(Obstacles[randomObstacle()], gameObject.transform);
                }
                if(!Camera.main.GetComponent<AudioSource>().isPlaying) Camera.main.GetComponent<AudioSource>().Play();

                if (SummonItem() == 1 && !gameEnded) Instantiate(Items[0], gameObject.transform);

            }
        }
    }

    int randomObstacle()
    {
        // 0 = lateral
        // 1 = vertical
        // 2 = cruzados
        // 3 = agujero (To do)
        // 4 = aire  (To do)
        // 5 = móviles (To do)
        
        
        return Random.Range(0, Obstacles.Length); 
    }

    int SummonItem()
    {
        //0 Nothing
        //1 Ball
        int result = Random.Range(0, 100);
        if (result > 85) return 1;
        return 0;
    }
    public void RestoreSpeed()
    {
        speed = savedSpeed;
        savedSpeed = 0;
    }
}
