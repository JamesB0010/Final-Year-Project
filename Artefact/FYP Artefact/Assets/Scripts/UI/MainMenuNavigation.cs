using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuNavigation : MonoBehaviour
{
    private SqueezeSelectButton [] playOptionContainers = new SqueezeSelectButton[2];
    
    private int leftSelectedContainer = 0;

    private int LeftSelectedContainer
    {
        get => this.leftSelectedContainer;
        set
        {
            bool noChangeDetected = this.leftSelectedContainer == value;
            if (noChangeDetected)
                return;

            this.playOptionContainers[this.leftSelectedContainer].RemoveFromClassList("Player1Focused");
            this.playOptionContainers[this.leftSelectedContainer].Player1SqueezeAmount = 0;

            this.leftSelectedContainer = value;

            if (value == -1)
            {
                this.leftSelectedContainer = 1;
            }

            this.leftSelectedContainer %= 2;

            this.playOptionContainers[this.leftSelectedContainer].AddToClassList("Player1Focused");
        }
    }
    private int rightSelectedContainer = 0;

    private int RightSelectedContainer
    {
        get => this.rightSelectedContainer;
        set
        {
            bool noChangeDetected = this.rightSelectedContainer== value;
            if (noChangeDetected)
                return;
 
            this.playOptionContainers[this.rightSelectedContainer].RemoveFromClassList("Player2Focused");
            this.playOptionContainers[this.rightSelectedContainer].Player2SqueezeAmount = 0;

            this.rightSelectedContainer= value;

            if (value == -1)
            {
                this.rightSelectedContainer= 1;
            }

            this.rightSelectedContainer %= 2;
            
            this.playOptionContainers[this.rightSelectedContainer].AddToClassList("Player2Focused");
        }
    }
    
    private void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        this.playOptionContainers[0] = root.Q<SqueezeSelectButton>("PlaySinglePlayerContainer");
        this.playOptionContainers[1] = root.Q<SqueezeSelectButton>("PlayMultiplayerContainer");
    }

    private void Update()
    {
        this.playOptionContainers[this.leftSelectedContainer].Player1SqueezeAmount = (uint)FingerTotalForceGetter.GetGenerousPullPercent(eteeAPI.LeftDevice); 
        this.playOptionContainers[this.rightSelectedContainer].Player2SqueezeAmount = (uint)FingerTotalForceGetter.GetGenerousPullPercent(eteeAPI.RightDevice); 
    }

    public void RightDeviceTrackpadDown() => this.RightSelectedContainer++;

    public void RightDeviceTrackpadUp() => this.RightSelectedContainer--;

    public void LeftDeviceTrackpadUp() => this.LeftSelectedContainer--;

    public void LeftDeviceTrackpadDown() => this.LeftSelectedContainer++;
}
