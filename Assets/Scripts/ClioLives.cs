using UnityEngine;
using UnityEngine.UI;

public class ClioLives : MonoBehaviour
{
    public int lives = 3;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private bool isInvincible = false;
    private float invincibilityDuration = 1f;
    private float invincibilityTimer = 0f;
    private float flashInterval = 0.2f;

    void Update()
    {
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0)
            {
                isInvincible = false;
                invincibilityTimer = 0f;
            }
            else
            {
                FlashClio();
            }
        }

        UpdateHeartDisplay();
    }

    public void RemoveLife()
    {
        if (!isInvincible)
        {
            lives--;
            isInvincible = true;
            invincibilityTimer = invincibilityDuration;

            if (lives <= 0)
            {
                GameManager.Instance.PlayerDies();
            }
        }
    }

    private void UpdateHeartDisplay()
    {
        foreach (Image img in hearts)
        {
            img.sprite = emptyHeart;
        }
        for (int i = 0; i < lives; i++)
        {
            hearts[i].sprite = fullHeart;
        }
    }

    private void FlashClio()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            float flashTimer = Time.time % flashInterval;
            if (flashTimer < flashInterval / 0.5f)
            {
                spriteRenderer.enabled = true;
            }
            else
            {
                spriteRenderer.enabled = false;
            }
        }
    }
}
