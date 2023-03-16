using UnityEngine;
using System.Collections;

public class TripleTriad : MonoBehaviour {

    public GameObject cardPrefab;  // Prefab for instantiating cards
    public GameObject[,] board;    // 3x3 grid for cards to be placed
    public int[] playerCards;      // Numerical values for human player's cards
    public int[] AI_Cards;         // Numerical values for AI player's cards

    void Start() {
        // Instantiate the 3x3 grid
        board = new GameObject[3,3];
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                board[i,j] = Instantiate(cardPrefab, new Vector3(i, 0, j), Quaternion.identity);
            }
        }

        // Initialize the player's cards and AI's cards
        playerCards = new int[] {5, 7, 8, 2, 10};
        AI_Cards = new int[] {4, 3, 6, 9, 1};

        // Place the first card for each player
        PlaceCard(playerCards[0], 0, 0, "Blue");
        PlaceCard(AI_Cards[0], 2, 2, "Red");
    }

    void PlaceCard(int cardValue, int x, int y, string team) {
        // Place the card on the board
        board[x,y].GetComponent<Card>().value = cardValue;
        board[x,y].GetComponent<Card>().team = team;

        // Check if any adjacent cards can be flipped
        FlipCards(x, y);
    }

    void FlipCards(int x, int y) {
        // Check north
        if (x > 0) {
            CheckAndFlip(x, y, x-1, y);
        }
        // Check south
        if (x < 2) {
            CheckAndFlip(x, y, x+1, y);
        }
        // Check west
        if (y > 0) {
            CheckAndFlip(x, y, x, y-1);
        }
        // Check east
        if (y < 2) {
            CheckAndFlip(x, y, x, y+1);
        }
    }

    void CheckAndFlip(int x1, int y1, int x2, int y2) {
        // Get the values of the two cards being compared
        int card1Value = board[x1,y1].GetComponent<Card>().value;
        int card2Value = board[x2,y2].GetComponent<Card>().value;

        // Compare the values and flip the card if necessary
        if (card1Value > card2Value) {
            board[x2,y2].GetComponent<Card>().team = board[x1,y1].GetComponent<Card>().team;
        }
    }

        void EndGame() {
        // Count the number of cards for each team
        int blueCards = 0;
        int redCards = 0;
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                if (board[i,j].GetComponent<Card>().team == "Blue") {
                    blueCards++;
                } else if (board[i,j].GetComponent<Card>().team == "Red") {
                    redCards++;
                }
            }
        }

        // Determine the winner and allow them to choose a card
        if (blueCards > redCards) {
            Debug.Log("Blue team wins!");
            // Allow the player to choose a card
        } else if (redCards > blueCards) {
            Debug.Log("Red team wins!");
            // Allow the AI to choose a card
        } else {
            Debug.Log("It's a tie!");
        }
    }
}

