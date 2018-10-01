using System;
using UnityEngine;

[Serializable]
public class Profile
{
    [Header("Settings")]
    [SerializeField] private string name;
    [SerializeField] private string description;

    [Header("Misc")]
    [SerializeField] private GameObject prefab;

    public Profile(string name, string description, float health, GameObject prefab)
    {
        this.name = name;
        this.description = description;

        this.prefab = prefab;
    }

    public string Name => name;

    public string Description => description;

    public GameObject Prefab => prefab;

    public override string ToString()
    {
        return name;
    }
}
