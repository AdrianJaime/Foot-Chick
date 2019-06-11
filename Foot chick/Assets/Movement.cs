using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Camera cam;
    private Vector3 firstCharacterPos, screenPos, move, firstFingerPos, endFingerPos, camFirstPos, limits, cameraLimits;
    private float dist, dragDistance;
    public bool shooting = false, released = false;
    Vector2 start = Vector2.zero, end = Vector2.zero, swipeDist = Vector2.zero;
    public List<int> swipeList;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        dist = Mathf.Abs(gameObject.transform.position.z - cam.transform.position.z);
        dragDistance = Screen.height * 20 / 100;
    }

    // Update is called once per frame
    void Update()
    {
        //Chutando balón
        if (shooting)
        {
            if (Input.touchCount > 0)
            {
                Touch swipe = Input.GetTouch(0);
                if (swipe.phase == TouchPhase.Began && !released) released = true;

                if (released)
                {
                    if (swipe.phase == TouchPhase.Began)
                    {
                        end = start = swipe.position;
                    }
                    if (swipe.phase == TouchPhase.Ended)
                    {
                        end = swipe.position;
                    }
                    swipeDist = end - start;
                    if (Mathf.Abs(swipeDist.x) > dragDistance || Mathf.Abs(swipeDist.y) > dragDistance)
                    {
                        swipeDist.Normalize();
                        CheckIfCorrect(swipeDist);
                    }
                }
            }

            //Al final resetear speed en ObstacleGenerator
        }
        //Control de movimiento del dedo sobre la pantalla
        else if (Input.touchCount > 0 && GameObject.Find("Obstacles").GetComponent<ObstacleGenerator>().gameStarted )
        {
            Touch touch = Input.GetTouch(0);
            screenPos = cam.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, dist));
            if (touch.phase == TouchPhase.Began)
            {
                if (limits == Vector3.zero) SetLimits();
                firstFingerPos = screenPos;
                firstCharacterPos = gameObject.transform.position;
                camFirstPos = cam.transform.position;

            }

            if (touch.phase == TouchPhase.Moved)
            {
                endFingerPos = screenPos;
                move = endFingerPos - firstFingerPos;
                gameObject.transform.position = firstCharacterPos + new Vector3(move.x, move.y, gameObject.transform.position.z);
                cam.transform.position = Vector3.Lerp(camFirstPos, camFirstPos + new Vector3(move.x, move.y, 0), 0.5f);
                Move();
            }
        }

    }

    void SetLimits()
    {
        limits = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 4 * 3, dist));
        cameraLimits = new Vector3(limits.x / 5, limits.y / 5, 0);
        Debug.Log(limits);
        Debug.Log(cameraLimits);
    }

    void Move()
    {
        //Use character limits

        if (gameObject.transform.position.x > limits.x)
            gameObject.transform.position = new Vector3(limits.x - 0.01f, gameObject.transform.position.y, gameObject.transform.position.z);
        if (gameObject.transform.position.x < -limits.x)
            gameObject.transform.position = new Vector3(-limits.x + 0.01f, gameObject.transform.position.y, gameObject.transform.position.z);
        if (gameObject.transform.position.y > limits.y - 0.5f)
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, limits.y - 0.51f, gameObject.transform.position.z);
        if (gameObject.transform.position.y < -limits.y)
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, -limits.y + 0.01f, gameObject.transform.position.z);

        //Use camera limits

        if (cam.transform.position.x > cameraLimits.x)
            cam.transform.position = new Vector3(cameraLimits.x - 0.01f, cam.transform.position.y, cam.transform.position.z);
        if (cam.transform.position.x < -cameraLimits.x)
            cam.transform.position = new Vector3(-cameraLimits.x + 0.01f, cam.transform.position.y, cam.transform.position.z);
        if (cam.transform.position.y > cameraLimits.y)
            cam.transform.position = new Vector3(cam.transform.position.x, cameraLimits.y - 0.01f, cam.transform.position.z);
        if (cam.transform.position.y < -cameraLimits.y)
            cam.transform.position = new Vector3(cam.transform.position.x, -cameraLimits.y + 0.01f, cam.transform.position.z);
    }

    private void FillSwipeList()
    {
        while (swipeList.Count < GameObject.Find("Obstacles").GetComponent<ObstacleGenerator>().difficulty)
        {
            swipeList.Add(Random.Range(0, 8));
        }
        GameObject.Find("Swipe Container").GetComponent<Swipes>().setSwipes(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle")) GameObject.Find("Heart Container").GetComponent<LifeManager>().LoseLife();
        if (other.CompareTag("Ball"))
        {
            shooting = true;
            FillSwipeList();
            // AÑADIR GRÁFICOS SWIPE
        }
    }
    
    private void CheckIfCorrect(Vector2 swipeDist)
    {
        int length = swipeList.Count;
        switch (swipeList[0])
        {
            case 0:
                if (swipeDist.y > 0 && swipeDist.x > -0.5f && swipeDist.x < 0.5f)
                    swipeList.RemoveAt(0);
                    // UP SWIPE
                break;
            case 1:
                if (swipeDist.y > 0 && swipeDist.x > 0 && swipeDist.x > 0.5f && swipeDist.y > 0.5f)
                    swipeList.RemoveAt(0);
                    // UP-RIGHT SWIPE
                break;
            case 2:
                if (swipeDist.x > 0 && swipeDist.y > -0.5f && swipeDist.y < 0.5f)
                    swipeList.RemoveAt(0);
                    // RIGHT SWIPE
                break;
            case 3:
                if (swipeDist.y < 0 && swipeDist.x > 0 && swipeDist.x > 0.5f && swipeDist.y < -0.5f)
                    swipeList.RemoveAt(0);
                    // DOWN-RIGHT SWIPE
                break;
            case 4:
                if (swipeDist.y < 0 && swipeDist.x > -0.5f && swipeDist.x < 0.5f)
                    swipeList.RemoveAt(0);
                    // DOWN SWIPE
                break;
            case 5:
                if (swipeDist.y < 0 && swipeDist.x < 0 && swipeDist.x < -0.5f && swipeDist.y < -0.5f)
                    swipeList.RemoveAt(0);
                    // DOWN-LEFT SWIPE
                break;
            case 6:
                if (swipeDist.x < 0 && swipeDist.y > -0.5f && swipeDist.y < 0.5f)
                    swipeList.RemoveAt(0);
                    // LEFT SWIPE
                break;
            case 7:
                if (swipeDist.y > 0 && swipeDist.x < 0 && swipeDist.x < -0.5f && swipeDist.y > 0.5f)
                    swipeList.RemoveAt(0);
                    // UP-LEFT SWIPE
                break;
            default:
                break;
        }
        if (length > swipeList.Count)
        {
            Debug.Log("Set");
            GameObject.Find("Swipe Container").GetComponent<Swipes>().setSwipes(true);
            if (swipeList.Count == 0)
            {
                shooting = false;
                GameObject.Find("Obstacles").GetComponent<ObstacleGenerator>().RestoreSpeed();
            }
        }
    }

}
