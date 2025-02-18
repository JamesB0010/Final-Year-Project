using System;
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

        private UxmlUnsignedIntAttributeDescription Player2SqueezeAmount = new UxmlUnsignedIntAttributeDescription()
        {
            name = "Player2SqueezeAmount", defaultValue = 0
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
            ate.Player2SqueezeAmount = Player2SqueezeAmount.GetValueFromBag(bag, cc);
        }
    }

    public SqueezeSelectButton()
    {
        Resources.Load<VisualTreeAsset>("UiToolkit/InteractionButton").CloneTree(this);
        base.AddToClassList("InteractionOption");
        this.buttonName = this.Q<TextElement>("ButtonText");
        this.player1SqueezeSlider = this.Q<VisualElement>("Slider1");
        this.player2SqueezeSlider = this.Q<VisualElement>("Slider2");
    }

    
    private TextElement buttonName;
    private VisualElement player1SqueezeSlider;
    private VisualElement player2SqueezeSlider;
    private bool pressed = false;
    public event Action ButtonPressed;

    private bool pressedPlayer1Only = false;
    public event Action Player1Pressed;
    
    private bool pressedPlayer2Only = false;
    public event Action Player2Pressed;

    public string ButtonTextValue
    {
        get => this.buttonName.text;
        set => this.buttonName.text = value;
    }

    public uint Player1SqueezeAmount
    {
        get => (uint)this.player1SqueezeSlider.style.width.value.value;
        set
        {
            this.player1SqueezeSlider.style.width = Length.Percent(value);
            this.CheckIfBothPlayersAreFullySqueezed();
        }
    }

    public uint Player2SqueezeAmount
    {
        get => (uint)this.player2SqueezeSlider.style.width.value.value;
        set
        {
            this.player2SqueezeSlider.style.width = Length.Percent(value);
            this.CheckIfBothPlayersAreFullySqueezed();
        }
    }

    private void CheckIfBothPlayersAreFullySqueezed()
    {
        if (this.Player1SqueezeAmount == 100 && this.Player2SqueezeAmount == 100)
        {
            if (this.pressed)
                return;

            this.pressed = true;
            
            ButtonPressed?.Invoke();
        }
        else
        {
            this.pressed = false;
            if (this.Player1SqueezeAmount == 100)
            {
                this.pressedPlayer2Only = false;
                if (pressedPlayer1Only)
                    return;

                this.pressedPlayer1Only = true;
                
                this.Player1Pressed?.Invoke();
            }

            if (this.Player2SqueezeAmount == 100)
            {
                this.pressedPlayer1Only = false;
                if (pressedPlayer2Only)
                    return;

                pressedPlayer2Only = true;
                
                this.Player2Pressed?.Invoke();
            }
        }
    }
}