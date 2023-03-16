using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    // Reference to the player's score text
    public Text playerScoreText;

    // Reference to the opponent's score text
    public Text opponentScoreText;

    // Reference to the message text
    public Text messageText;

    public void UpdatePlayerScore(int score)
    {
        playerScoreText.text = "Player Score: " + score;
    }

    public void UpdateOpponentScore(int score)
    {
        opponentScoreText.text = "Opponent Score: " + score;
    }

    public void ShowMessage(string message)
    {
        messageText.text = message;
    }

    public void HideMessage()
    {
        messageText.text = "";
    }
}

