using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string name;

    [Space(10)]
    [SerializeField] private float time = 10.0f;
    [SerializeField] private float velocity = 10.0f;

    private void Update()
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {
            Destroy(gameObject);
        }
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward * 3.0f, Time.deltaTime * velocity);
    }

    public string Name => name;
}
