
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private GameObject pauseButton;

   

    public void OnPauseButtonClick()
    {
        Time.timeScale = 0f;
        uiPanel.SetActive(true);
        pauseButton?.SetActive(false);
    }

    public void OnResumeButtonClick()
    {
        Time.timeScale = 1f;
        pauseButton?.SetActive(true);
        uiPanel?.SetActive(false);
    }

    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
     }
}
