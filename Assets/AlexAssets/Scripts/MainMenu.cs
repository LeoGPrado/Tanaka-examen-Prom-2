using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] Button playButton;
    [SerializeField] Button exitButton;
    void Awake()
    {
        if (playButton != null) playButton.onClick.AddListener(()=> LoadGameScene());
        if (exitButton != null) exitButton.onClick.AddListener(() => ExitGame());
    }
    void LoadGameScene() => SceneManager.LoadScene(sceneName);
    void ExitGame() => Application.Quit();
}
