using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicRing : MonoBehaviour
{
    [Header("Type Ring")]
    public string type;

    [Header("Sound Enter")]
    public AudioClip audioClip;

    [Header("PLayer Elevation")]
    private Transform player;
    public float speed;
    private GameObject playerGO;
    public bool toElevate;

    [Header("Stop Timer")]
    private Timer timer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerGO = GameObject.FindGameObjectWithTag("Player");
        timer = GameObject.FindGameObjectWithTag("StopTimer").GetComponent<Timer>();
        toElevate = false;
    }

    void Update()
    {
        if (toElevate)
            ElevationPlayer();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (type == "EVAC")
            {
                timer.stopTimer = true;
                if (audioClip != null)
                    AudioSource.PlayClipAtPoint(audioClip, transform.position);

                RemovePlayerMovements();
                toElevate = true;
            }
        }
    }

    void RemovePlayerMovements()
    {
        playerGO.GetComponent<MovementsStateManager>().enabled = false;
        playerGO.GetComponent<ActionStateManager>().enabled = false;
        playerGO.GetComponent<Animator>().SetBool("Running", false);
        playerGO.GetComponent<Animator>().SetBool("Walking", false);
        playerGO.GetComponent<Animator>().SetBool("happyIdle", true);
    }

    void ElevationPlayer()
    {
        float movement = speed * Time.deltaTime;
        player.position = new Vector3(player.position.x, player.position.y + movement, player.position.z);
    }
}
