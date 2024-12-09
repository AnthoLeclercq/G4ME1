using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Fire Rate")]
    [SerializeField] float fireRate;
    [SerializeField] bool semiAuto;
    float fireRateTimer;

    [Header("Bullet Properties")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelPos;
    [SerializeField] float bulletVelocity;
    [SerializeField] int bulletsPerShot;
    public int damage = 20;
    AimStateManager aim;

    [Header("Fire Sound")]
    [SerializeField] AudioClip gunShot;
    private AudioSource audioSource;

    private ActionStateManager actions;
    private WeaponAmmo ammo;
    private WeaponBloom bloom;

    [Header("Ragdoll")]
    public float enemyKickbackForce = 100;

    [Header("NPC Read")]
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        aim = GetComponentInParent<AimStateManager>();
        ammo = GetComponent<WeaponAmmo>();
        bloom = GetComponent<WeaponBloom>();
        actions = GetComponentInParent<ActionStateManager>();
        fireRateTimer = fireRate;
        Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        Transform panelAmmo = canvas.transform.Find("PanelDialogue");
        animator = panelAmmo.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldFire())
            Fire();
    }

    bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate)
            return false;
        if (ammo.currentAmmo == 0)
            return false;
        if (actions.currentState == actions.Reload)
            return false;
        if (semiAuto && Input.GetKeyDown(KeyCode.Mouse0) && animator.GetBool("isOpen") == false)
            return true;
        if (!semiAuto && Input.GetKey(KeyCode.Mouse0) && animator.GetBool("isOpen") == false)
            return true;
        return false;
    }

    void Fire()
    {
        fireRateTimer = 0;
        audioSource.PlayOneShot(gunShot);
        barrelPos.LookAt(aim.aimPos);
        barrelPos.localEulerAngles = bloom.BloomAngle(barrelPos);
        ammo.currentAmmo--;
        for(int i = 0 ; i < bulletsPerShot ; i++)
        {
            GameObject currentBullet = Instantiate(bullet, barrelPos.position, barrelPos.rotation);

            Bullet bulletScript = currentBullet.GetComponent<Bullet>();
            bulletScript.weapon = this;

            bulletScript.dir = barrelPos.transform.forward;

            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPos.forward * bulletVelocity, ForceMode.Impulse);
        }
    }
}
