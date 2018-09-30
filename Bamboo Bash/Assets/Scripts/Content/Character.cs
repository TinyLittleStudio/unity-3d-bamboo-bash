using System;
using TinyLittleStudio.BambooBash.Utils;
using UnityEngine;

namespace TinyLittleStudio.BambooBash.Content
{
    public class Character : MonoBehaviour
    {
        private static readonly float HEALTH_BAR_LENGTH = 3.0f;

        [Header("Settings")]
        [SerializeField] private GameObject target;
        [SerializeField] private GameObject modelTarget;
        [SerializeField] private GameObject healthBar;

        [Space(10)]
        [SerializeField] private float rate, next;

        private bool isInitialized = false;

        private void Awake()
        {
            if (target == null || modelTarget == null || healthBar == null)
            {
                throw new Exception("Missing Elements On Character-Script!");
            }
        }

        private void Update()
        {
            if (TouchUtils.Gestures.IsUp)
            {
                if (Time.time > next)
                {
                    Projectile projectile = Profile.Projectile;

                    if (projectile != null)
                    {
                        Instantiate(projectile, target.transform.position + target.transform.forward, target.transform.rotation);
                    }
                    next = Time.time + rate;
                }
            }

            if (TouchUtils.Gestures.IsLeft || TouchUtils.Gestures.IsRight)
            {
                Notification.Notify("Du hast dir selbst Schaden zugefügt?", Notification.Level.WARNING);
                Damage(10.0f);
            }

            if (TouchUtils.Gestures.IsDoubleTap)
            {
                Notification.Notify("Du bist gestorben!", Notification.Level.ERROR);
                Die();
            }

            CurrentHealth = Mathf.Clamp(CurrentHealth, MinHealth, MaxHealth);

            if (healthBar != null)
            {
                float length = (CurrentHealth / MaxHealth) * 100.0f * (Character.HEALTH_BAR_LENGTH / 100.0f);

                healthBar.transform.localScale = new Vector3(length, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
                healthBar.transform.LookAt(Camera.main.transform.position);
            }
        }

        public void OnProfileChange()
        {
            if (Profile != null && !isInitialized)
            {
                GameObject model = Instantiate(Profile.Prefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
                model.transform.SetParent(modelTarget.transform);
                model.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

                CurrentHealth = Profile.Health;
            }
        }

        public void Damage(float damage)
        {
            CurrentHealth -= damage;

            if (CurrentHealth <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            Manager.DefaultInstance.DisableAR();

            Destroy(this.gameObject);

            Debug.Log("Destroy: " + this.gameObject);

            Manager.DefaultInstance.EndScreen.SetActive(true);
        }

        public float CurrentHealth { get; set; }

        public float MinHealth => 0;

        public float MaxHealth => Profile.Health;

        public Profile Profile { get; set; }
    }
}
