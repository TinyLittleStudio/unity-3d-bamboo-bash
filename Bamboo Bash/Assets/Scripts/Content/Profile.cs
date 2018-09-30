using System;
using UnityEngine;

[Serializable]
public class Profile
{
    public static readonly float DEFAULT_HEALTH = 10.0f;

    [Header("Settings")]
    [SerializeField] private string name;
    [SerializeField] private string description;

    [SerializeField] private float health;

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

    public override string ToString()
    {
        return name;
    }
}
