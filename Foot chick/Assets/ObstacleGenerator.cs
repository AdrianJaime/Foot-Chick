using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject[] Obstacles;
    public float speed = 1;
    public Camera cam;
    private bool gameStarted;
    public GameObject lastObject;

    public Vector2 lateralSize, verticalSize;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (gameStarted)
            {
                speed += Time.deltaTime * 0.1f;
                if (lastObject.transform.position.z < 5)
                {
                    lastObject = Instantiate(Obstacles[randomObstacle()], gameObject.transform);
                }
            }
            if (touch.phase == TouchPhase.Began && !gameStarted)
            {
                gameStarted = true;
                lastObject = Instantiate(Obstacles[randomObstacle()], gameObject.transform);
            }
        }
    }

    int randomObstacle()
    {
        // 0 = lateral
        // 1 = vertical
        // 2 = cruzados
        // 3 = agujero
        // 4 = aire  (To do)
        // 5 = móviles (To do)
        
        
        return Random.Range(0, Obstacles.Length); 
    }
}
