using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cinematics : MonoBehaviour
{
    public GameObject[] text;
    public float textSpeed;
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(0, -Screen.width * 2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Tutorial", 0) == 0 && !text[0].activeSelf) text[0].SetActive(true);
        else if(!text[1].activeSelf && PlayerPrefs.GetInt("Tutorial", 0) != 0) text[1].SetActive(true);

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0, Screen.width * 2, 0), Time.deltaTime * textSpeed);

        if (PlayerPrefs.GetInt("Tutorial", 0) == 0 && transform.localPosition.y == Screen.width * 2) SceneManager.LoadScene("Gameplay");
        else SceneManager.LoadScene("Main Menu");

    }
}
