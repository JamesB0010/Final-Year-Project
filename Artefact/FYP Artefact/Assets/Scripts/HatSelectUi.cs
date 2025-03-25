using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class HatSelectUi : MonoBehaviour
{
    private VisualElement root;
    
    private VisualElement SqueezeAmountBar;
    private VisualElement hatThumbnail;

    [SerializeField] private HatSelectGameplayGameMode hatSelectGameplayMode;

    [SerializeField] private Sprite noHatSprite;
    [SerializeField] private Sprite[] hatSprites;

    [SerializeField] private UnityEvent hatSelected;

    [SerializeField] private PlayerHatDataHolder playerHatData;
    
    private void Awake()
    {
        this.root = GetComponent<UIDocument>().rootVisualElement;

        this.SqueezeAmountBar = this.root.Q<VisualElement>("BarFill");
        this.hatThumbnail = this.root.Q<VisualElement>("Thumbnail");
    }

    private void Update()
    {
        float squeezePercent = FingerTotalForceGetter.GetGenerousPullPercent(this.hatSelectGameplayMode.PlayerDevice);
        this.SqueezeAmountBar.style.width = Length.Percent(squeezePercent);

        if (squeezePercent == 100)
        {
            PlayerPrefs.SetInt("leftControllerHatIndex", this.playerHatData.leftControllerHatIndex);
            PlayerPrefs.SetInt("rightControllerHatIndex", this.playerHatData.rightControllerHatIndex);
            this.hatSelected?.Invoke();
        }
    }

    public void NoHatSelected()
    {
        hatThumbnail.style.backgroundImage = new StyleBackground(this.noHatSprite);
    }

    public void HatSelected(int index)
    {
        hatThumbnail.style.backgroundImage = new StyleBackground(this.hatSprites[index]);
    }
}
