using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

public class MainMenuButtonReactions : MonoBehaviour
{
    [SerializeField] private GameModeHolder gameModeHolder;
    [SerializeField] private UnityEvent buttonPressed;
    [SerializeField] private UnityEvent HatSelectionPressedEvent;
    public void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        
        SqueezeSelectButton singlePlayerButton = root.Q<SqueezeSelectButton>("PlaySinglePlayerContainer");
        singlePlayerButton.Player1Pressed += () =>
        {
            this.PlaySinglePlayerPressed(eteeAPI.LeftDevice);
            this.buttonPressed?.Invoke();
        };

        singlePlayerButton.Player2Pressed += () =>
        {
            this.PlaySinglePlayerPressed(eteeAPI.RightDevice);
            this.buttonPressed?.Invoke();
        };
        
        root.Q<SqueezeSelectButton>("PlayMultiplayerContainer").ButtonPressed += () =>
        {
            this.gameModeHolder.GameMode = Resources.Load<GameMode>("GameModes/Multiplayer");
            this.buttonPressed?.Invoke();
        };

        SqueezeSelectButton hatSelectionButton = root.Q<SqueezeSelectButton>("HatSelectionContainer");
        hatSelectionButton.Player1Pressed += () =>
        {
            this.HatSelectionPressed(eteeAPI.LeftDevice);
            this.buttonPressed?.Invoke();
        };

        hatSelectionButton.Player2Pressed += () =>
        {
            this.HatSelectionPressed(eteeAPI.RightDevice);
            this.buttonPressed?.Invoke();
        };
    }
    private void PlaySinglePlayerPressed(eteeDevice device)
    {
        var gameMode = Resources.Load<GameMode>("GameModes/SinglePlayer");
        this.gameModeHolder.GameMode = gameMode;
        ((SinglePlayerGameMode)gameMode).Initialize(device);
    }
    
    private void HatSelectionPressed(eteeDevice device)
    {
        GetComponent<SignalReceiver>().ChangeReactionAtIndex(0, this.HatSelectionPressedEvent);
    }
}
