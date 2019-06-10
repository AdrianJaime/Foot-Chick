using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public Vector2 move, firstPos, endPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Control de movimiento del dedo sobre la pantalla
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                firstPos = touch.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                endPos = touch.position;
                move = endPos - firstPos;
            }
        }
    }
}
