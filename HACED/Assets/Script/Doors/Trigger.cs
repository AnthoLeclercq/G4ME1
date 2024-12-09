using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public Transform door;
    public float speed;
    public float maxOpenValue;

    private float currentValue = 0f;
    private bool opening = false;
    private bool closing = false;

    void Update()
    {
        if (opening)
            OpenDoor();
        if (closing)
            CloseDoor();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            opening = true;
            closing = false;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            opening = false;
            closing = true;
        }
    }

    void OpenDoor()
    {
        float movement = speed * Time.deltaTime;
        currentValue += movement;
        if (currentValue <= maxOpenValue)
            door.position = new Vector3(door.position.x, door.position.y + movement, door.position.z);
        else
            opening = false;
    }

    void CloseDoor()
    {
        float movement = speed * Time.deltaTime;
        currentValue -= movement;
        if (currentValue >= 0)
            door.position = new Vector3(door.position.x, door.position.y - movement, door.position.z);
        else
            closing = false;
    }
}
