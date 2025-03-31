using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class EndScreenMultiplayerUi : MonoBehaviour
{
    private VisualElement root;

    [SerializeField] private PlayerScores playerScores;
    private SqueezeSelectButton ReturnToMenuButton { get; set; }

    [SerializeField] private UnityEvent ReturnToMenuPressed;

    private void Awake()
    {
        this.root = GetComponent<UIDocument>().rootVisualElement;

        this.root.Q<TextElement>("CaughtAmountPlayer1").text = $"You caught {playerScores.Player1Score} fish!";

        this.root.Q<TextElement>("CaughtAmountPlayer2").text = $"You caugt {playerScores.Player2Score} fish!";

        this.ReturnToMenuButton = this.root.Q<SqueezeSelectButton>("ReturnToMainMenu");

        this.ReturnToMenuButton.ButtonPressed += () => { this.ReturnToMenuPressed?.Invoke(); };
    }


    private void Update()
    {
        this.ReturnToMenuButton.Player1SqueezeAmount =
            (uint)FingerTotalForceGetter.GetGenerousPullPercent(eteeAPI.LeftDevice);
        this.ReturnToMenuButton.Player2SqueezeAmount =
            (uint)FingerTotalForceGetter.GetGenerousPullPercent(eteeAPI.RightDevice);
    }
    
    public void ShowNewHighScoreText()
    {
        this.root.Q<TextElement>("NewHighscoreText").style.visibility = Visibility.Visible;
    }
}