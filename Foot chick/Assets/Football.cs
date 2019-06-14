using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Football : MonoBehaviour
{
    float width, height;
    Camera cam;
    ObstacleGenerator obsData;
    // Start is called before the first frame update
    void Start()
    {
        obsData = GetComponentInParent<ObstacleGenerator>();
        cam = obsData.cam;
        Vector2 pos = searchPosition();
        gameObject.transform.position = new Vector3(pos.x, pos.y, 15);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -obsData.speed);
        if (gameObject.transform.position.z < 0) gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -obsData.speed * 5);
        if (gameObject.transform.position.z < -10 && gameObject != obsData.lastObject) Destroy(gameObject);
    }

    Vector2 searchPosition()
    {
        float Sizey = cam.orthographicSize * 2;
        float Sizex = Sizey * Screen.width / Screen.height;
        int loopStop = 0;
        while (true)
        {
            int randNumb = Random.Range(1, 5);

            // Vertical obstacles

            if (obsData.lastObject.name == "Clouds(Clone)" && obsData.lastObject.transform.position.y == obsData.lastObject.GetComponent<Vertical>().height)
            {
                if (randNumb == 1)
                    return new Vector2(-Sizex / 4, Sizey / 6);
                if (randNumb == 2)
                    return new Vector2(Sizex / 4, Sizey / 6);
            }
            if (obsData.lastObject.name == "Clouds(Clone)" && obsData.lastObject.transform.position.y == -obsData.lastObject.GetComponent<Vertical>().height)
            {
                if (randNumb == 3)
                    return new Vector2(-Sizex / 4, -Sizey / 3.5f);
                if (randNumb == 4)
                    return new Vector2(Sizex / 4, -Sizey / 3.5f);
            }

            // Horizontal obstacles

            if (obsData.lastObject.name == "Horizontal(Clone)" && obsData.lastObject.transform.position.x == obsData.lastObject.GetComponent<LateralObstacle>().width + 1)
            {
                if (randNumb == 1)
                    return new Vector2(-Sizex / 4, Sizey / 6f);
                if (randNumb == 3)
                    return new Vector2(-Sizex / 4 - 1, -Sizey / 3.5f);
            }
            if (obsData.lastObject.name == "Horizontal(Clone)" && obsData.lastObject.transform.position.x == -obsData.lastObject.GetComponent<LateralObstacle>().width)
            {
                if (randNumb == 2)
                    return new Vector2(Sizex / 4, Sizey / 6f);
                if (randNumb == 4)
                    return new Vector2(Sizex / 4 + 1, -Sizey / 3.5f);
            }
            if(loopStop == 100)
            {
                Debug.Log("trapped");
                return new Vector2(100000,100000);
            }
            loopStop++;

        }
    }
}
