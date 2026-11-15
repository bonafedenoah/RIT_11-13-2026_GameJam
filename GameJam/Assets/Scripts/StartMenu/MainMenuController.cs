using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject controlPanel;
    private bool isControl = false;

    [SerializeField] private GameObject creditsPanel;
    private bool isCredit = false;

    public void StartGame()
    {
        Debug.Log("Starting the game...");
        SceneManager.LoadScene("Scene_01");
    }

    public void QuitGame()
    {
        Debug.Log("Game is quitting...");
        Application.Quit();
    }

    public void ClickControlPanel()
    {
        bool isActive = controlPanel.activeSelf;
        controlPanel.SetActive(!isActive);
    }

    public void ClickCreditsPanel()
    {
        bool isActive = creditsPanel.activeSelf;
        creditsPanel.SetActive(!isActive);
    }
}