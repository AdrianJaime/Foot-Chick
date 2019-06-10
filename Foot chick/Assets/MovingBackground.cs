using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackground : MonoBehaviour
{
    bool gameStarted, right = false;
    public float smooth, dist;
    public Camera cam;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        dist = Mathf.Abs(gameObject.transform.position.z - cam.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) gameStarted = true;
        }
        if (gameStarted)
        {
            time += Time.deltaTime;
            if(right) gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, cam.ScreenToWorldPoint(new Vector3(Screen.width, cam.WorldToScreenPoint(gameObject.transform.position).y, dist)), time * smooth);
            else gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, cam.ScreenToWorldPoint(new Vector3(0, cam.WorldToScreenPoint(gameObject.transform.position).y, dist)), time * smooth);
            if (time * smooth > 0.001)
            {
                ResetTravel();
            }
        }
        
            
    }

    void ResetTravel()
    {
        if (right) right = false;
        else right = true;
        time = 0;
    }
}
