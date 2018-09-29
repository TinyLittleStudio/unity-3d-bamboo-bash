using UnityEngine;
using UnityEngine.UI;
using TinyLittleStudio.BambooBash.Utils;

public class Character : MonoBehaviour
{
    public static readonly float DEFAULT_HEALTH = 10.0f;

    [SerializeField] private float health, maxHealth;

    private Image healthBar;

    private void Awake()
    {
        this.health = Character.DEFAULT_HEALTH;
        this.maxHealth = Character.DEFAULT_HEALTH;
    }

    private void Update()
    {
        if (TouchUtils.Gestures.IsLeft)
        {
            Debug.Log("Left Swipe");
        }

        if (TouchUtils.Gestures.IsRight)
        {
            Debug.Log("Right Swipe");
        }
    }

    public void Damage(float damage)
    {
        health -= damage;

        if(healthBar != null) {
            healthBar.fillAmount = health / maxHealth;
        }

        if (health <= 0)
        {
            health = 0;
            Die();
        } else if(health > maxHealth) {
            health = maxHealth;
        }
    }

    public void Die()
    {
        Manager.DefaultInstance.EndScreen.SetActive(true);

        Manager.DefaultInstance.DisableAR();

        Destroy(this.gameObject);
    }

    public float Health => health;
}
