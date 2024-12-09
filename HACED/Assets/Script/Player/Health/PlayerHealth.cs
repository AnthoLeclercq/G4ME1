using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Bar")]
    public int maxHealth = 100;
    public int currentHealth;
    private HealthBar healthBar;

    private DeathManager deathManager;

    private Image overlay;
    [Header("Damage Overlay")]
    public float duration; //how long the image stays fully opaque
    public float fadeSpeed;
    private float durationTimer;

    private Timer timer;
    
    void Start()
    {
        currentHealth = maxHealth;
        healthBar = FindObjectOfType<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);
        deathManager = GameObject.Find("Death_canvas").GetComponent<DeathManager>();
        GameObject panelBlood = GameObject.Find("Canvas/PanelBlood");
        overlay = panelBlood.GetComponent<Image>();
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
        timer = GameObject.FindGameObjectWithTag("StopTimer").GetComponent<Timer>();
    }

    void Update()
    {
        CheckHealth();  
        if (Input.GetKeyDown(KeyCode.H) && currentHealth < maxHealth && currentHealth > 0)
            UseFirstAid();
        
        if (overlay.color.a > 0)
        {
            if (currentHealth < 30)
                return; //don't fade the panelBlood if low hp
            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                //fade the image
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }
    }

    public void TakeHealth(int health)
    {
        currentHealth += health;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        healthBar.SetHealth(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
            Die();

        durationTimer = 0;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0.7f);
    }

    void CheckHealth()
    {
        if (currentHealth > 0)
            deathManager.SetActiveDeath(false);
        else
            Die();
    }

    public void Die()
    {
        deathManager.SetActiveDeath(true);
        GetComponent<MovementsStateManager>().enabled = false;
        GetComponent<AimStateManager>().enabled = false;
        GetComponent<ActionStateManager>().enabled = false;
        timer.stopTimer = true;
    }

    void UseFirstAid()
    {
        Inventory inventory = Inventory.instance;
        if (inventory.health > 0)
        {
            TakeHealth(50);
            inventory.RemoveHealth(1);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        EnemyHealth zombieHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (zombieHealth != null && zombieHealth.health > 0)
        {
            switch (collision.transform.name)
            {
                case "ZombieTypeA(Clone)":
                    TakeDamage(20);
                    break;
                case "ZombieTypeB(Clone)":
                    TakeDamage(15);
                    break;
                case "ZombieTypeC(Clone)":
                    TakeDamage(15);
                    break;
                default:
                    break;
            }
        }   
    }
}
