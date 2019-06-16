using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeAnimations : MonoBehaviour
{

    public float animationTime, passedTime;
    public bool showing;
    // Start is called before the first frame update
    void Start()
    {
        showing = false;
        passedTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (showing)
        {
            passedTime += Time.deltaTime;
            Debug.Log(passedTime);
            if (passedTime >= animationTime)
            {
                showing = false;
                transform.localScale = new Vector3(0, 1, 1);
            }
        }
    }
}
