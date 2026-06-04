using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsToMainMenu : MonoBehaviour
{
    [SerializeField] string mainMenuSceneName = "MainScene";
    [SerializeField] Button returnButton;
    void Awake()
    {
        if (returnButton != null) returnButton.onClick.AddListener(() => ReturnToMainMenu());
    }
    void ReturnToMainMenu() => SceneManager.LoadScene(mainMenuSceneName);
}
