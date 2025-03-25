using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class HatDatabase : ScriptableObject
{
    [SerializeField] private GameObject[] hatPrefabs;

    public GameObject GetHat(int index)
    {
        if (index == -1)
            return null;
        
        return this.hatPrefabs[index];   
    }
}
