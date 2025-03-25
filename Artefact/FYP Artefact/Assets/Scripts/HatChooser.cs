using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HatChooser : MonoBehaviour
{
    [SerializeField] private Transform hatSlot;
    [SerializeField] private GameObject[] HatPrefabs;

    public UnityEvent<int> newHatSelected;
    public UnityEvent noHatSelected;

    private GameObject activeHat;

    private int hatIndex = -1;
    public int HatIndex
    {
        get => this.hatIndex;

        set
        {
            if (value >= this.HatPrefabs.Length)
                value = -1;
            if (value < -1)
                value = this.HatPrefabs.Length - 1;
            
            this.hatIndex = value;
            
            this.EquipHat(this.hatIndex);
        }
    }

    public void PreviousHat() => this.HatIndex--;

    public void NextHat() => this.HatIndex++;

    private void EquipHat(int index)
    {
        if (activeHat != null)
        {
            Destroy(this.activeHat);
        }

        if (index == -1)
        {
            this.noHatSelected?.Invoke();
            return;
        }
        
        this.activeHat = Instantiate(HatPrefabs[index], hatSlot);
        this.activeHat.AddComponent<ItemShopItemChangeItemEffect>();
        this.newHatSelected?.Invoke(index);
    }
}
