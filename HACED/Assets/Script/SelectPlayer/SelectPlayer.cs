using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectPlayer : MonoBehaviour
{
    GameObject spotlight;

    void Start()
    {
        spotlight = GameObject.Find("Spotlight").gameObject;
        Debug.Log(PlayerPrefs.GetString("Player"));
    }

    void OnMouseEnter()
    {
        spotlight.GetComponent<AudioSource>().Play();
        spotlight.transform.position = new Vector3(transform.position.x, spotlight.transform.position.y, spotlight.transform.position.z);
    }

    void OnMouseDown()
    {
        PlayerPrefs.SetString("Player", gameObject.name);
        SceneManager.LoadScene(2);
    }
}
