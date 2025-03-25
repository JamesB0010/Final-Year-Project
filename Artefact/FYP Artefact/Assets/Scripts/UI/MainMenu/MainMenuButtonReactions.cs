using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

public class MainMenuButtonReactions : MonoBehaviour
{
    [SerializeField] private GameModeHolder gameModeHolder;
    [SerializeField] private UnityEvent buttonPressed;
    [SerializeField] private UnityEvent HatSelectionPressedEvent;
    [SerializeField] private PlayerHatDataHolder playerHatData;
    [SerializeField] private HatDatabase hatDb;
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
            MultiplayerGameplayGameMode gameMode = Resources.Load<MultiplayerGameplayGameMode>("GameModes/Multiplayer");
            this.gameModeHolder.GameplayGameMode = gameMode;
            gameMode.player1Hat = this.hatDb.GetHat(this.playerHatData.leftControllerHatIndex);
            gameMode.player2Hat = this.hatDb.GetHat(this.playerHatData.rightControllerHatIndex);
            
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
        var gameMode = Resources.Load<SinglePlayerGameplayGameMode>("GameModes/SinglePlayer");
        this.gameModeHolder.GameplayGameMode = gameMode;
        if (device.isLeft)
        {
            gameMode.playerHat = hatDb.GetHat(this.playerHatData.leftControllerHatIndex);
        }
        else
        {
            gameMode.playerHat = hatDb.GetHat(this.playerHatData.rightControllerHatIndex);
        }
        gameMode.Initialize(device);
    }
    
    private void HatSelectionPressed(eteeDevice device)
    {
        Resources.Load<HatSelectGameplayGameMode>("GameModes/HatSelectGameMode").Initialize(device);
        GetComponent<SignalReceiver>().ChangeReactionAtIndex(0, this.HatSelectionPressedEvent);
    }
}
