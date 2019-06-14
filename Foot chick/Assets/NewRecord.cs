using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRecord : MonoBehaviour
{
    public float speed, time;
    bool right = true;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (right) gameObject.transform.Rotate(new Vector3(0, 0, speed));
        else gameObject.transform.Rotate(new Vector3(0, 0, -speed));
        if (time >= 0.5)
        {
            if (right) right = false;
            else right = true;
            time = 0;
        }
    }
}
