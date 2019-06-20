using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    private Camera cam;
    private Vector3 firstCharacterPos, screenPos, move, firstFingerPos, endFingerPos, camFirstPos, limits, cameraLimits;
    private float dist, dragDistance;
    public bool shooting = false, released = false, paused = false;
    Vector2 start = Vector2.zero, end = Vector2.zero, swipeDist = Vector2.zero;
    public List<int> swipeList;
    public Sprite[] swipeAnimations, swipeLifesSprites;
    public GameObject shootingBall, swipeLifesImage, trail, actualTrail;
    public int swipeLifes, changedSL;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        dist = Mathf.Abs(gameObject.transform.position.z - cam.transform.position.z);
        dragDistance = Screen.height * 10 / 100;
    }

    // Update is called once per frame
    void Update()
    {
        //Chutando balón
        if (paused) ;
        else if (shooting)
        {
            if (swipeLifes != changedSL) swipeLifesChange();
            if (Input.touchCount > 0)
            {
                Touch swipe = Input.GetTouch(0);
                if (swipe.phase == TouchPhase.Began && !released) released = true;

                if (released)
                {
                    if (swipe.phase == TouchPhase.Began)
                    {
                        actualTrail = Instantiate(trail, cam.ScreenToWorldPoint(new Vector3(swipe.position.x, swipe.position.y, dist)), new Quaternion(0,0,0,0));

                        actualTrail.transform.position = new Vector3(actualTrail.transform.position.x, actualTrail.transform.position.y, 0);
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
                actualTrail.transform.position = cam.ScreenToWorldPoint(new Vector3(swipe.position.x, swipe.position.y, dist));
                actualTrail.transform.position = new Vector3(actualTrail.transform.position.x, actualTrail.transform.position.y, 0);
            }

            //Al final resetear speed en ObstacleGenerator
        }
        //Control de movimiento del dedo sobre la pantalla
        else if (Input.touchCount > 0)
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
        if (other.CompareTag("Obstacle"))
        {
            GameObject.Find("Heart Container").GetComponent<LifeManager>().LoseLife();
            Destroy(other.gameObject);
            GetComponent<AudioSource>().Play();
        }
        if (other.CompareTag("Ball"))
        {
            shooting = true;
            FillSwipeList();
            released = false;
            shootingBall = other.gameObject;
            changedSL = 0;
            swipeLifes = 3;
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
                    DoSwipe(swipeList[0]);
                else swipeLifes--;
                // UP SWIPE
                break;
            case 1:
                if (swipeDist.y > 0 && swipeDist.x > 0 && swipeDist.x > 0.5f && swipeDist.y > 0.5f)
                    DoSwipe(swipeList[0]);
                else swipeLifes--;
                // UP-RIGHT SWIPE
                break;
            case 2:
                if (swipeDist.x > 0 && swipeDist.y > -0.5f && swipeDist.y < 0.5f)
                    DoSwipe(swipeList[0]);
                else swipeLifes--;
                // RIGHT SWIPE
                break;
            case 3:
                if (swipeDist.y < 0 && swipeDist.x > 0 && swipeDist.x > 0.5f && swipeDist.y < -0.5f)
                    DoSwipe(swipeList[0]);
                else swipeLifes--;
                // DOWN-RIGHT SWIPE
                break;
            case 4:
                if (swipeDist.y < 0 && swipeDist.x > -0.5f && swipeDist.x < 0.5f)
                    DoSwipe(swipeList[0]);
                else swipeLifes--;
                // DOWN SWIPE
                break;
            case 5:
                if (swipeDist.y < 0 && swipeDist.x < 0 && swipeDist.x < -0.5f && swipeDist.y < -0.5f)
                    DoSwipe(swipeList[0]);
                else swipeLifes--;
                // DOWN-LEFT SWIPE
                break;
            case 6:
                if (swipeDist.x < 0 && swipeDist.y > -0.5f && swipeDist.y < 0.5f)
                    DoSwipe(swipeList[0]);
                else swipeLifes--;
                // LEFT SWIPE
                break;
            case 7:
                if (swipeDist.y > 0 && swipeDist.x < 0 && swipeDist.x < -0.5f && swipeDist.y > 0.5f)
                    DoSwipe(swipeList[0]);
                else swipeLifes--;
                // UP-LEFT SWIPE
                break;
            default:
                break;
        }
        if(swipeLifes == 0)
        {
            swipeList.Clear();
        }
        if (length > swipeList.Count)
        {
            GameObject.Find("Swipe Container").GetComponent<Swipes>().setSwipes(true);
            if (swipeList.Count == 0)
            {
                shooting = false;
                GameObject.Find("Obstacles").GetComponent<ObstacleGenerator>().RestoreSpeed();
                if (swipeLifes == 3) GameObject.Find("Points").GetComponent<PointFixer>().points += 5;
                else if (swipeLifes == 2) GameObject.Find("Points").GetComponent<PointFixer>().points += 2;
                else if(swipeLifes == 1)GameObject.Find("Points").GetComponent<PointFixer>().points++;
                GameObject.Find("Points").GetComponent<PointFixer>().UpdatePoints();
                Destroy(shootingBall);
                swipeLifesImage.transform.localScale = new Vector3(0, 1, 1);
                for (int i = 0; i < GameObject.Find("Swipe Container").GetComponent<Swipes>().swipeArr.Length; i++)
                    GameObject.Find("Swipe Container").GetComponent<Swipes>().swipeArr[i].GetComponent<Image>().color = new Color(GameObject.Find("Swipe Container").GetComponent<Swipes>().swipeArr[i].GetComponent<Image>().color.r, GameObject.Find("Swipe Container").GetComponent<Swipes>().swipeArr[i].GetComponent<Image>().color.g, GameObject.Find("Swipe Container").GetComponent<Swipes>().swipeArr[i].GetComponent<Image>().color.b, 1);

            }
        }
    }

    private void DoSwipe(int swipe)
    {
        swipeList.RemoveAt(0);
        GameObject.Find("SwipeAnimation").GetComponent<SwipeAnimations>().passedTime = 0;
        GameObject.Find("SwipeAnimation").GetComponent<SwipeAnimations>().showing = true;
        GameObject.Find("SwipeAnimation").transform.localScale = new Vector3(1, 1, 1);
        GameObject.Find("SwipeAnimation").GetComponent<Image>().sprite = swipeAnimations[swipe];
    }

    private void swipeLifesChange()
    {
        swipeLifesImage.GetComponent<Image>().sprite = swipeLifesSprites[swipeLifes - 1];
        if (swipeLifesImage.transform.localScale.x == 0)
        {
            swipeLifesImage.transform.localScale = new Vector3(1, 1, 1);
        }
        changedSL = swipeLifes;
    }

}
