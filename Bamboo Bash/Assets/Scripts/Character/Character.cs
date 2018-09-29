using UnityEngine;

public class Character : MonoBehaviour
{
    public static readonly float DEFAULT_HEALTH = 10.0f;

    [SerializeField] private float health;

    private void Awake()
    {
        this.health = Character.DEFAULT_HEALTH;
    }

    private void Update()
    {

    }

    public void Damage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    public void Die()
    {
        Manager.DefaultInstance.EndScreen.SetActive(true);

        Destroy(this.gameObject);
    }

    public float Health => health;
}
