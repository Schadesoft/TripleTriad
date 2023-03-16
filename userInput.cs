using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    // Reference to the GameManager script
    public GameManager gameManager;

    void Update()
    {
        // Check if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse position
            Vector2 mousePos = Input.mousePosition;

            // Check if the mouse position is over a UI element
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                // Convert the mouse position to world coordinates
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

                // Send a raycast from the camera to the mouse position
                RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

                // Check if the raycast hit a card
                if (hit.collider != null && hit.collider.CompareTag("Card"))
                {
                    // Get the card script from the hit collider
                    Card card = hit.collider.GetComponent<Card>();

                    // Check if the card belongs to the current player
                    if (card.isPlayerCard)
                    {
                        // Check if the card is already on the board
                        if (!card.isOnBoard)
                        {
                            // If the card is not on the board, let the player place it on the board
                            gameManager.PlaceCard(card);
                        }
                    }
                }
            }
        }
    }
}


