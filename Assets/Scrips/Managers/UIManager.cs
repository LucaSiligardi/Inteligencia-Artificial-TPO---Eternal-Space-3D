using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

    [Header("Shot UI")]
    [SerializeField] private Image shotImage;
    [SerializeField] private Sprite normalShotSprite;
    [SerializeField] private Sprite powerShotSprite;
    [SerializeField] private Image cooldownFill;
    [SerializeField] private GameObject startMessage;
    [SerializeField] private float messageDuration = 3f;
    void Start()
    {
        if (startMessage != null)
        {
            startMessage.SetActive(true);
            Invoke(nameof(HideStartMessage), messageDuration);
        }
    }
    void OnEnable()
    {
        if (GameMananger.Instance != null)
            GameMananger.Instance.OnScoreChanged += UpdateScoreUI;
    }

    void OnDisable()
    {
        if (GameMananger.Instance != null)
            GameMananger.Instance.OnScoreChanged -= UpdateScoreUI;
    }

    private void HideStartMessage()
    {
        if (startMessage != null)
            startMessage.SetActive(false);
    }

    private void UpdateScoreUI(int newScore)
    {
        if (scoreText != null)
            scoreText.text = "Puntos: " + newScore;
    }

    public void UpdateShotUI(bool isPowerShot)
    {
        if (shotImage == null) return;

        shotImage.sprite = isPowerShot ? powerShotSprite : normalShotSprite;
    }
    public void UpdateCooldown(float normalizedValue)
    {
        if (cooldownFill == null) return;

        cooldownFill.fillAmount = normalizedValue;
    }
}