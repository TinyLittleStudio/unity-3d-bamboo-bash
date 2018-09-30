using System;
using TinyLittleStudio;
using UnityEngine;

[Serializable]
public class Profile
{
    public static readonly float DEFAULT_HEALTH = 10.0f;

    [Header("Settings")]
    [SerializeField] private string name;
    [SerializeField] private string description;

    [SerializeField] private float health;

    [SerializeField] private string projectile;

    [Header("Misc")]
    [SerializeField] private GameObject prefab;

    public Profile(string name, string description, GameObject prefab) : this(name, description, Profile.DEFAULT_HEALTH, prefab)
    {

    }

    public Profile(string name, string description, float health, GameObject prefab)
    {
        this.name = name;
        this.description = description;

        this.health = health;

        this.prefab = prefab;
    }

    public string Name => name;

    public string Description => description;

    public float Health => health;

    public GameObject Prefab => prefab;

    public Projectile Projectile
    {
        get
        {
            foreach (Projectile projectile in Manager.DefaultInstance.Projectiles)
            {
                if (projectile.name == this.projectile)
                {
                    return projectile;
                }
            }
            return null;
        }
    }

    public override string ToString()
    {
        return name;
    }
}
