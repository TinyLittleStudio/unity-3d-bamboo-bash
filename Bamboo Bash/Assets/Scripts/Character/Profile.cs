using System;
using UnityEngine;

[Serializable]
public class Profile {
    [SerializeField] private string name;
    [SerializeField] private GameObject prefab;

    public Profile(string name, GameObject prefab)
    {
        this.name = name;
        this.prefab = prefab;
    }

    public string Name => name;

    public GameObject Prefab => prefab;

    public override string ToString()
    {
        return name;
    }
}
