using System.Linq.Expressions;
using UnityEngine;
using System.Collections;

public class PickUpObject : MonoBehaviour
{
    [Header("Type Object")]
    public string type;
    
    [Header("PickUp Sound")]
    public AudioClip audioClip;

    [Header("PickUp Ammo Check")]
    private WeaponAmmo weaponAmmo;

    [Header("Respawn")]
    public bool respawn;
    public float delayRespawn;

    void Start() => weaponAmmo = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponAmmo>();

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {         
            if(type == "AMMO" && (weaponAmmo.currentAmmo != weaponAmmo.clipSize || weaponAmmo.extraAmmo != weaponAmmo.maxExtraAmmo))
            {
                AudioSource.PlayClipAtPoint(audioClip, transform.position);
                Inventory.instance.RefillAmmo();
                ObjectManager();

            }

            if (type == "HEALTH")
            {
                AudioSource.PlayClipAtPoint(audioClip, transform.position);
                Inventory.instance.AddHealth(1);
                ObjectManager();
            }           
        }
    }


    private void ObjectManager()
    {
        if (respawn)
        {
            gameObject.SetActive(false);
            Invoke(nameof(Respawn), delayRespawn);
        }
        else
            Destroy(gameObject);
    }

    private void  Respawn() => gameObject.SetActive(true);
}
