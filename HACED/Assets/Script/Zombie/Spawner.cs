using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Zombie")]
    public GameObject zombieTypeA;
    public GameObject zombieTypeB;
    public GameObject zombieTypeC;

    [Header("Interval before first spawn")]
    public float spawnStart = 0f;

    [Header("Interval between spawn")]
    public float spawnInterval = 2f;

    [Header("MAX spawn Limit")]
    private int spawnCount = 0;
    private int nbTotalSpawn = 0;
    public int nbZombieTypeA = 0;
    public int nbZombieTypeB = 0; 
    public int nbZombieTypeC = 0;

    [Header("Player")]
    private GameObject player;

    [Header("Min Distance spawn")]
    public float minDistance = 30f;

    [Header("Random interval spawn")]
    public float randomInterval = 3f;


    void Start()
    {
        nbTotalSpawn = nbZombieTypeA + nbZombieTypeB + nbZombieTypeC;
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(spawnZomby(spawnInterval, zombieTypeA, zombieTypeB, zombieTypeC));
    }

    private IEnumerator spawnZomby(float interval, GameObject zombieA, GameObject zombieB, GameObject zombieC)
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < minDistance)
        {
            yield return new WaitForSeconds(spawnStart);

            for (int i = 0; i < nbTotalSpawn; i++)
            {
                spawnCount++;
                if (nbZombieTypeA > 0)
                {
                    InstanciateZombieWithRandomPosition(zombieA, interval);
                    nbZombieTypeA--;
                    yield return new WaitForSeconds(interval);
                }

                if (nbZombieTypeB > 0)
                {
                    InstanciateZombieWithRandomPosition(zombieB, interval);
                    nbZombieTypeB--;
                    yield return new WaitForSeconds(interval);
                }

                if (nbZombieTypeC > 0)
                {
                    InstanciateZombieWithRandomPosition(zombieC, interval);
                    nbZombieTypeC--;
                    yield return new WaitForSeconds(interval);
                }
            }
        
        }

        yield return new WaitForSeconds(1);

        if (spawnCount < 1)
            StartCoroutine(spawnZomby(interval, zombieA, zombieB, zombieC));

    }

    private void InstanciateZombieWithRandomPosition(GameObject zombie, float interval)
    {
        Vector3 randomPosition = transform.position;
        randomPosition.x = randomPosition.x + Random.Range(-randomInterval, randomInterval);
        Instantiate(zombie, randomPosition, Quaternion.identity);
    }
}
