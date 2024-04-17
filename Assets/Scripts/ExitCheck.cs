using UnityEngine;

public class ExitCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player collided with exit");

            if (GameManager.Instance.coinsCollected >= GameManager.Instance.totalCoinsInLevel)
            {
                Debug.Log("All coins collected. Returning to the main menu...");
                GameManager.Instance.PlayerPassesExitCheck();
            }
            else
            {
                Debug.Log("Not all coins collected");
                GameManager.Instance.PlayerPassesExitCheck();
            }
        }
    }
}
