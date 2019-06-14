using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertical : MonoBehaviour
{
    public float width, height;
    Camera cam;
    ObstacleGenerator obsData;
    // Start is called before the first frame update
    void Start()
    {
        obsData = gameObject.GetComponentInParent<ObstacleGenerator>();
        cam = obsData.cam;
        width = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, Mathf.Abs(gameObject.transform.position.z - cam.transform.position.z))).x;
        height = cam.ScreenToWorldPoint(new Vector3(Screen.height, 0, Mathf.Abs(gameObject.transform.position.z - cam.transform.position.z))).y;
        float Sizey = cam.orthographicSize * 2;
        float Sizex = Sizey * Screen.width / Screen.height;
        int rand = Random.Range(0, 2);
        gameObject.transform.position = new Vector3(0, rand == 0 ? height : -height, 15);
        if (rand == 0) gameObject.transform.Rotate(new Vector3(180, 0, 0));
        gameObject.transform.localScale = new Vector3(Sizex/150 , Sizey/150 , gameObject.transform.localScale.z);


    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -obsData.speed);
        if (gameObject.transform.position.z < 0) gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -obsData.speed * 5);
        if (gameObject.transform.position.z < -10 && gameObject != obsData.lastObject)
        {
            GameObject.Find("Points").GetComponent<PointFixer>().points++;
            GameObject.Find("Points").GetComponent<PointFixer>().UpdatePoints();
            Destroy(gameObject);
        }
    }
}