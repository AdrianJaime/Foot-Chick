using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject finger, mask;
    public float movement = 200;
    public float distance, speed;
    private bool startedTutorial = false;
    private int oldI = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Character").GetComponent<Movement>().swipeList.Count > 0 && PlayerPrefs.GetInt("Tutorial", 0) == 0)
        {
            startedTutorial = true;
            finger.SetActive(true);
            mask.SetActive(true);
            int i = GameObject.FindGameObjectWithTag("Character").GetComponent<Movement>().swipeList[0];
            if (i != oldI) finger.transform.localPosition = Vector3.zero;
            oldI = i;
            switch (i)
            {
                case 0:
                    if (finger.transform.localPosition == Vector3.zero) finger.transform.localPosition = new Vector3(0, -movement, 0);
                    if (Vector3.Distance(finger.transform.localPosition, new Vector3(0, movement, 0)) < distance) finger.transform.localPosition = new Vector3(0, -movement, 0);
                    finger.transform.localPosition = Vector3.MoveTowards(finger.transform.localPosition, new Vector3(0, movement, 0), speed * Time.deltaTime);
                    break;
                case 1:
                    if (finger.transform.localPosition == Vector3.zero) finger.transform.localPosition = new Vector3(-movement, -movement, 0);
                    if (Vector3.Distance(finger.transform.localPosition, new Vector3(movement, movement, 0)) < distance) finger.transform.localPosition = new Vector3(-movement, -movement, 0);
                    finger.transform.localPosition = Vector3.MoveTowards(finger.transform.localPosition, new Vector3(movement, movement, 0), speed * Time.deltaTime);
                    break;
                case 2:
                    if (finger.transform.localPosition == Vector3.zero) finger.transform.localPosition = new Vector3(-movement, 0, 0);
                    if (Vector3.Distance(finger.transform.localPosition, new Vector3(movement, 0, 0)) < distance) finger.transform.localPosition = new Vector3(-movement, 0, 0);
                    finger.transform.localPosition = Vector3.MoveTowards(finger.transform.localPosition, new Vector3(movement, 0, 0), speed * Time.deltaTime);
                    break;
                case 3:
                    if (finger.transform.localPosition == Vector3.zero) finger.transform.localPosition = new Vector3(-movement, movement, 0);
                    if (Vector3.Distance(finger.transform.localPosition, new Vector3(movement, -movement, 0)) < distance) finger.transform.localPosition = new Vector3(-movement, movement, 0);
                    finger.transform.localPosition = Vector3.MoveTowards(finger.transform.localPosition, new Vector3(movement, -movement, 0), speed * Time.deltaTime);
                    break;
                case 4:
                    if (finger.transform.localPosition == Vector3.zero) finger.transform.localPosition = new Vector3(0, movement, 0);
                    if (Vector3.Distance(finger.transform.localPosition, new Vector3(0, -movement, 0)) < distance) finger.transform.localPosition = new Vector3(0, movement, 0);
                    finger.transform.localPosition = Vector3.MoveTowards(finger.transform.localPosition, new Vector3(0, -movement, 0), speed * Time.deltaTime);
                    break;
                case 5:
                    if (finger.transform.localPosition == Vector3.zero) finger.transform.localPosition = new Vector3(movement, movement, 0);
                    if (Vector3.Distance(finger.transform.localPosition, new Vector3(-movement, -movement, 0)) < distance) finger.transform.localPosition = new Vector3(movement, movement, 0);
                    finger.transform.localPosition = Vector3.MoveTowards(finger.transform.localPosition, new Vector3(-movement, -movement, 0), speed * Time.deltaTime);
                    break;
                case 6:
                    if (finger.transform.localPosition == Vector3.zero) finger.transform.localPosition = new Vector3(movement, 0, 0);
                    if (Vector3.Distance(finger.transform.localPosition, new Vector3(-movement, 0, 0)) < distance) finger.transform.localPosition = new Vector3(movement, 0, 0);
                    finger.transform.localPosition = Vector3.MoveTowards(finger.transform.localPosition, new Vector3(-movement, 0, 0), speed * Time.deltaTime);
                    break;
                case 7:
                    if (finger.transform.localPosition == Vector3.zero) finger.transform.localPosition = new Vector3(movement, -movement, 0);
                    if (Vector3.Distance(finger.transform.localPosition, new Vector3(-movement, movement, 0)) < distance) finger.transform.localPosition = new Vector3(movement, -movement, 0);
                    finger.transform.localPosition = Vector3.MoveTowards(finger.transform.localPosition, new Vector3(-movement, movement, 0), speed * Time.deltaTime);
                    break;
                default:
                    if (finger.transform.localPosition == Vector3.zero) finger.transform.localPosition = new Vector3(0, movement, 0);
                    if (Vector3.Distance(finger.transform.localPosition, new Vector3(0, -movement, 0)) < distance) finger.transform.localPosition = new Vector3(0, movement, 0);
                    finger.transform.localPosition = Vector3.MoveTowards(new Vector3(0, finger.transform.localPosition.y, 0), new Vector3(0, -movement, 0), speed * Time.deltaTime);
                    break;
            }
        }
        else if (startedTutorial)
        {
            PlayerPrefs.SetInt("Tutorial", 1);
            startedTutorial = false;
            finger.SetActive(false);
            mask.SetActive(false);
        }

    }
}
