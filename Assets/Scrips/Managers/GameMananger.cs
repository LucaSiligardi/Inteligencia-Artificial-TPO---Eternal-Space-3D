using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMananger : MonoBehaviour
{
    public static GameMananger Instance;

    public event Action<int> OnScoreChanged;
    private int score;
    public event Action OnVictory;
    private int victoryScore = 2000;
    private bool hasWon = false;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;

            OnVictory += Victory;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        OnVictory -= Victory;
        SceneManager.sceneLoaded -= OnSceneLoaded; 
    }
    public void AddPoints(int amount)
    {
        score += amount;
        OnScoreChanged?.Invoke(score);

        //EVENTO DE VICTORIA
        if (!hasWon && score >= victoryScore)
        {
            hasWon = true;
            OnVictory?.Invoke(); 
        }
    }

    public void ResetScore()
    {
        score = 0;
        OnScoreChanged?.Invoke(score);
    }

    public int GetScore() => score;




    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (AudioManager.Instance != null)
        {
            if (scene.name == "Menu")
            {
                AudioManager.Instance.PlayMusic(AudioManager.MusicType.Menu);
            }
            else if (scene.name == "Game")
            {
                AudioManager.Instance.PlayMusic(AudioManager.MusicType.Game);
            }
            else
            {
               
                AudioManager.Instance.PlayMusic(AudioManager.MusicType.Menu);
            }
        }

        if (scene.name == "Victory")
        {
            Button menuBtn = GameObject.Find("MenuButton")?.GetComponent<Button>();
            Button quitBtn = GameObject.Find("QuitButton")?.GetComponent<Button>();

            if (menuBtn != null) menuBtn.onClick.AddListener(LoadMenu);
            if (quitBtn != null) quitBtn.onClick.AddListener(QuitGame);
        }
        else if (scene.name == "Defeat")
        {
            Button retryBtn = GameObject.Find("RetryButton")?.GetComponent<Button>();
            Button menuBtn = GameObject.Find("MenuButton")?.GetComponent<Button>();

            if (retryBtn != null) retryBtn.onClick.AddListener(LoadGame);
            if (menuBtn != null) menuBtn.onClick.AddListener(LoadMenu);
        }
    }



    //memento 
    public ScoreMemento CreateMemento()
    {
        return new ScoreMemento(score);
    }

    private void Victory()
    {
        Debug.Log("Victoria");
        SceneManager.LoadScene("Victory");
    }

    public void LoadMenu()
    {
      
        SceneManager.sceneLoaded -= OnSceneLoaded;

       
        SceneManager.LoadScene("Menu");

        
        Destroy(gameObject);
    }

    public void LoadGame()
    {
        ResetScore();
        hasWon = false;
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

    public void LoadDefeat()
    {
        Debug.Log("Derrota");
        SceneManager.LoadScene("Defeat");
    }
}