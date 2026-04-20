using UnityEngine;

public class MenuUIController : MonoBehaviour
{
    public GameObject tutorialPanel;

    public void ShowTutorial()
    {
        tutorialPanel.SetActive(true);
    }

    public void HideTutorial()
    {
        tutorialPanel.SetActive(false);
    }
}
