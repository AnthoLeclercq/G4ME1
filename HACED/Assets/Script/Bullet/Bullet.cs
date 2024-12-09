using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float timeToDestroy;
    [HideInInspector] public WeaponManager weapon;
    [HideInInspector] public Vector3 dir;

    public GameObject ImpactsParticles;

    void Start() => Destroy(this.gameObject, timeToDestroy);

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<EnemyHealth>())
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponentInParent<EnemyHealth>();
            enemyHealth.TakeDamage(weapon.damage);

            if (enemyHealth.health <= 0 && enemyHealth.isDead == false)
            {
                Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
                rb.AddForce(dir * weapon.enemyKickbackForce, ForceMode.Impulse);
                enemyHealth.isDead = true;
            }
        }

        if (collision.gameObject.tag == "Impactable")
            Instantiate(ImpactsParticles, transform.position, transform.rotation);

        Destroy(this.gameObject);
    }
}
