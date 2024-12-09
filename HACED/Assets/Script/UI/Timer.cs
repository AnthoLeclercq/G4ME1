using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float time = 300f; //5min
    private float maxTime = 0f;
    private GameObject player;

    public bool stopTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stopTimer = false;
        maxTime = time;
    }

    private IEnumerator TimerDown()
    {
        while (time > 0)
        {
            if (!stopTimer)
                time--;
            yield return new WaitForSeconds(1);
            GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>().text = string.Format("{0:0}:{1:00}", Mathf.Floor(time/60), time%60);
        }

        if (time == 0)
            player.GetComponent<PlayerHealth>().Die();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && time == maxTime) {
            StartCoroutine(TimerDown());
        }
            
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("StopTimer").Length; i++) {
                GameObject.FindGameObjectsWithTag("StopTimer")[i].GetComponent<BoxCollider>().enabled = false;
            }
            
        }
    }
}
