using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public Vector3 firstCharacterPos, screenPos, move, firstFingerPos, endFingerPos;

    public Camera cam;

    public float dist;
    // Start is called before the first frame update
    void Start()
    {
        dist = Mathf.Abs(gameObject.transform.position.z - cam.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

        //Control de movimiento del dedo sobre la pantalla
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            screenPos = cam.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, dist));
            Debug.Log(screenPos);
            if (touch.phase == TouchPhase.Began)
            {
                firstFingerPos = screenPos;
                firstCharacterPos = gameObject.transform.position;

            }

            if (touch.phase == TouchPhase.Moved)
            {
                endFingerPos = screenPos;
                move = endFingerPos - firstFingerPos;
             
                Debug.Log(firstFingerPos);
                gameObject.transform.position = firstCharacterPos + new Vector3(move.x, move.y, gameObject.transform.position.z) ;
            }
        }

    }
}
