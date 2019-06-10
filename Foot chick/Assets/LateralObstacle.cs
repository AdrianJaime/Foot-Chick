﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateralObstacle : MonoBehaviour
{
    float width, height;
    Camera cam;
    ObstacleGenerator obsData;
    // Start is called before the first frame update
    void Start()
    {
        obsData = gameObject.GetComponentInParent<ObstacleGenerator>();
        cam = obsData.cam;
        width = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, Mathf.Abs(gameObject.transform.position.z - cam.transform.position.z))).x;
        height = cam.ScreenToWorldPoint(new Vector3(Screen.height, 0, Mathf.Abs(gameObject.transform.position.z - cam.transform.position.z))).y;
        gameObject.transform.position = new Vector3(Random.Range(0, 2) == 0 ? width - 5 : -width + 5 , 0, 15);
        gameObject.transform.localScale = new Vector3(obsData.lateralSize.x, obsData.lateralSize.y, gameObject.transform.localScale.z);


    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -obsData.speed);
        if(gameObject.transform.position.z < 0) gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -obsData.speed * 5);
    }

    private void OnBecameInvisible()
    {
        if(gameObject != obsData.lastObject)
            Destroy(gameObject);
    }
}
