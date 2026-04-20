using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Configuración de Vida")]
    public int maxLives = 3;
    private int currentLives;

    [Header("Referencias UI")]
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Start()
    {
        currentLives = maxLives;
        UpdateHeartsUI();
    }

    public void TakeDamage(int amount)
    {
        currentLives -= amount;
        if (currentLives < 0) currentLives = 0;

        UpdateHeartsUI();

        if (currentLives <= 0)
        {
            SaveScore();

            GameMananger.Instance.LoadDefeat();
        }
    }

    private void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentLives)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }
    //memento pattern
    private void SaveScore()
    {
        GameMananger gm = FindObjectOfType<GameMananger>();
        if (gm != null)
        {
            ScoreMemento current = gm.CreateMemento();
            ScoreMemento previous = ScoreCareTaker.Load();

            // Solo guardar si el puntaje es mayor
            if (current.bestScore > previous.bestScore)
            {
                ScoreCareTaker.Save(current);
            }
        }
    }
}