using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string gameNameScene;
    [SerializeField] string creditsNameScene;
    [SerializeField] Button playButton;
    [SerializeField] Button exitButton;
    [SerializeField] Button creditsButton;
    void Awake()
    {
        if (playButton != null) playButton.onClick.AddListener(()=> LoadGameScene());
        if (exitButton != null) exitButton.onClick.AddListener(() => ExitGame());
        if (creditsButton != null) creditsButton.onClick.AddListener(() => LoadCreditsScene());
    }
    void LoadGameScene() => SceneManager.LoadScene(gameNameScene);
    void LoadCreditsScene() => SceneManager.LoadScene(creditsNameScene);
    void ExitGame() => Application.Quit();
}
