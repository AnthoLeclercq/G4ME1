using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int health = 0;

    public static Inventory instance;
    public Text FirstAidText;

    private GameObject currentWeapon;
    [HideInInspector] public WeaponAmmo ammo;

    public void Start()
    {
        currentWeapon = GameObject.FindGameObjectWithTag("Weapon");
        ammo = currentWeapon.GetComponent<WeaponAmmo>();
    }

    private void Awake()
    {   
        if(instance != null)
            return;

        instance = this; 
    }

    public void AddHealth(int count)
    {
        health += count;
        FirstAidText.text = health.ToString();
    }

    public void RemoveHealth(int count)
    {
        health -= count;
        FirstAidText.text = health.ToString();
    }

    public void RefillAmmo() => ammo.RefillAmmo();
}
