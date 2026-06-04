using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] Button resumeButton;
    [SerializeField] Button exitToMainMenuBtn;
    [SerializeField] Button quitButton;
    [SerializeField] string sceneName = "MainScene";
    [SerializeField] KeyCode toggleKey = KeyCode.Escape;
    bool isPaused = false;
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (pausePanel != null) pausePanel.SetActive(false);

        if (resumeButton != null) resumeButton.onClick.AddListener(() => TogglePause());
        if (quitButton != null) quitButton.onClick.AddListener(() => QuitGame());
        if (exitToMainMenuBtn != null) exitToMainMenuBtn.onClick.AddListener(() => GoToMainMenu());
    }
    void Update()
    {
        if (Input.GetKeyDown(toggleKey)) TogglePause();
    }
    void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
    }
    void GoToMainMenu()
    {
        SceneManager.LoadScene(sceneName);
    }
    void TogglePause()
    {
        isPaused = !isPaused;
        if (pausePanel != null) pausePanel.SetActive(isPaused);
        Cursor.visible = isPaused;
        Cursor.lockState = !isPaused ?  CursorLockMode.Locked : CursorLockMode.None;
        Time.timeScale = isPaused ? 0 : 1;
    }
}
