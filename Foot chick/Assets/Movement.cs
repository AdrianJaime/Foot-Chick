using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Camera cam;

    private Vector3 firstCharacterPos, screenPos, move, firstFingerPos, endFingerPos, camFirstPos;
    private Vector3 limits, cameraLimits;
    private float dist;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        dist = Mathf.Abs(gameObject.transform.position.z - cam.transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {

        //Control de movimiento del dedo sobre la pantalla
        if (Input.touchCount > 0)
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
        Debug.Log(limits);
        Debug.Log(cameraLimits);
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
}
