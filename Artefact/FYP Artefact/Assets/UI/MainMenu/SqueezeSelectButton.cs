using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

class SqueezeSelectButton : VisualElement
{
    public new class UxmlFactory : UxmlFactory<SqueezeSelectButton, UxmlTraits> { }

    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlStringAttributeDescription ButtonTextValue =
            new UxmlStringAttributeDescription { name = "Button_Text", defaultValue = "default_value" };

        private UxmlUnsignedIntAttributeDescription Player1SqueezeAmount = new UxmlUnsignedIntAttributeDescription()
        {
            name = "Player1SqueezeAmount", defaultValue = 0
        };

        public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
        {
            get { yield break; }
        }

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as SqueezeSelectButton;

            ate.ButtonTextValue = ButtonTextValue.GetValueFromBag(bag, cc);
            ate.Player1SqueezeAmount = Player1SqueezeAmount.GetValueFromBag(bag, cc);
        }
    }

    public SqueezeSelectButton()
    {
        Resources.Load<VisualTreeAsset>("UiToolkit/InteractionButton").CloneTree(this);
        base.AddToClassList("InteractionOption");
        this.buttonName = this.Q<TextElement>("ButtonText");
        this.player1SqueezeSlider = this.Q<VisualElement>("Slider1");
    }

    
    private TextElement buttonName;
    private VisualElement player1SqueezeSlider;
    private VisualElement player2SqueezeSlider;

    public string ButtonTextValue
    {
        get => this.buttonName.text;
        set
        {
            this.buttonName.text = value;
        }
    }

    private uint player1SqueezeAmount = 0;

    public uint Player1SqueezeAmount
    {
        get => this.player1SqueezeAmount;
        set
        {
            this.player1SqueezeAmount = value;

            float pixelWidthOfParent = this.player1SqueezeSlider.parent.resolvedStyle.width;
            uint percentageMultiplier = value / 100;
            this.player1SqueezeSlider.style.width = pixelWidthOfParent * percentageMultiplier;
        }
    }
}