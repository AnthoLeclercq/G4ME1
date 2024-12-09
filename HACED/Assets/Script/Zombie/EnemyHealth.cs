using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public float health;
    private RagdollManager ragdollManager;
    [HideInInspector] public bool isDead;
    string objectName;

    void Start()
    {
        ragdollManager = GetComponent<RagdollManager>();
        objectName = gameObject.name;
    }

    public void TakeDamage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            if(health <= 0)
                StartCoroutine(EnemyDeath());
        }
        
    }

    IEnumerator EnemyDeath()
    {

        ragdollManager.TriggerRagdoll();
        GetComponent<Animator>().enabled = false;
        GetComponent<EnemyManager>().enabled = false;
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;


        yield return new WaitForSeconds(10);
        Destroy(this.gameObject);
    }
}
