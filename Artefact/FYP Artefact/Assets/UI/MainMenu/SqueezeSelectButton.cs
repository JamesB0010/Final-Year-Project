using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

class SqueezeSelectButton : VisualElement
{
    public new class UxmlFactory : UxmlFactory<SqueezeSelectButton, UxmlTraits> { }

    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlStringAttributeDescription m_String =
            new UxmlStringAttributeDescription { name = "Button_Text", defaultValue = "default_value" };

        public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
        {
            get { yield break; }
        }

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as SqueezeSelectButton;

            ate.ButtonTextValue = m_String.GetValueFromBag(bag, cc);
        }
    }

    public SqueezeSelectButton()
    {
        Resources.Load<VisualTreeAsset>("UiToolkit/InteractionButton").CloneTree(this);
        base.AddToClassList("InteractionOption");
        this.buttonName = this.Q<TextElement>("ButtonText");
    }

    [SerializeField] private int something;
    
    private TextElement buttonName;

    public string ButtonTextValue
    {
        get => this.buttonName.text;
        set
        {
            this.buttonName.text = value;
        }
    }
}