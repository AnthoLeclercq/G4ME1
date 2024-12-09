using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlayer : MonoBehaviour
{
    public GameObject[] players;
    void Awake()
    {
        string player = PlayerPrefs.GetString("Player");
        for (int i = 0; i < players.Length; i++)
        {
            if (player == players[i].name)
                Instantiate(players[i], transform.position, Quaternion.identity);
        }
    }
}
