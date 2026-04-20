using UnityEngine;
using UnityEngine.UI;  
using TMPro;            

public class MainMenuScoreDisplay : MonoBehaviour
{
    [Header("UI")]
 
    public TMP_Text scoreTMPText;   

    private void Start()
    {
        // Cargar puntaje 
        ScoreMemento saved = ScoreCareTaker.Load();

        
        if (scoreTMPText != null)
        {
            scoreTMPText.text = "Mejor puntaje: " + saved.bestScore;
        }
    }
}