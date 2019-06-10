using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateralObstacle : MonoBehaviour
{
    double height;
    float width;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        height = cam.orthographicSize * 2.0;
        //width = cam.orthographicSize * 2.0 * Screen.width / Screen.height;
        width = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, Mathf.Abs(gameObject.transform.position.z - cam.transform.position.z))).x;
        gameObject.transform.position = new Vector3(width, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
