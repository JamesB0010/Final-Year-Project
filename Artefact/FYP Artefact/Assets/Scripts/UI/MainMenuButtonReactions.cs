using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class MainMenuButtonReactions : MonoBehaviour
{
    [SerializeField] private GameModeHolder gameModeHolder;
    [SerializeField] private UnityEvent buttonPressed;
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
    }

    private void PlaySinglePlayerPressed(eteeDevice device)
    {
        var gameMode = Resources.Load<GameMode>("GameModes/SinglePlayer");
        this.gameModeHolder.GameMode = gameMode;
        ((SinglePlayerGameMode)gameMode).Initialize(device);
    }
}
