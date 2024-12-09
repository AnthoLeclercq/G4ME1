using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    private EndManager endManager;
    private bool isInside = false;

    private MagicRing magicRing;

    void Start()
    {
        magicRing = GameObject.FindGameObjectWithTag("EvacuateZone").GetComponent<MagicRing>();
        endManager = GameObject.Find("End_canvas").GetComponent<EndManager>();
    }

    void Update() => endManager.SetActiveEnd(isInside);

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
            isInside = true;
        magicRing.toElevate = false;
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
            isInside = false;
    }
}
