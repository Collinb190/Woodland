using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int lives { get; private set; }
    public int coinsCollected { get; private set; }
    public int totalCoinsInLevel = 3;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        lives = 3;
        coinsCollected = 0;
        StartCoroutine(StartSceneRoutine());
    }

    private IEnumerator StartSceneRoutine()
    {
        if (SceneManager.GetActiveScene().name != "Woodland-1")
        {
            Debug.Log("Starting scene routine...");
            yield return new WaitForSeconds(5f); // Wait for 5 seconds
            Debug.Log("Loading Woodland-1...");
            LoadLevel("Woodland-1");
        }
    }

    private void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void PlayerDies()
    {
        Debug.Log("Player died! Loading DeathScene...");
        LoadLevel("DeathScene");
    }


    public void PlayerPassesExitCheck()
    {
        Debug.Log("Player passed exit check!");
        if (coinsCollected >= totalCoinsInLevel)
        {
            Debug.Log("All coins collected! Loading RealityScene...");
            LoadLevel("RealityScene");
        }
        else
        {
            Debug.Log("Not all coins collected! Loading BrokeScene...");
            LoadLevel("BrokeScene");
        }
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("Returning to main menu...");
        LoadLevel("MainMenu");
    }

    public void CollectCoin()
    {
        Debug.Log("Coin collected!");
        coinsCollected++;
        if (coinsCollected >= totalCoinsInLevel)
        {
            Debug.Log("All coins collected!");
        }
    }
}
