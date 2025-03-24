using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatChooser : MonoBehaviour
{
    [SerializeField] private Transform hatSlot;
    [SerializeField] private GameObject[] HatPrefabs;

    private GameObject activeHat;


    public void EquipHat(int index)
    {
        if (activeHat != null)
        {
            Destroy(this.activeHat);
        }

        this.activeHat = Instantiate(HatPrefabs[index], hatSlot);
    }
}
