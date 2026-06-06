using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : BaseManager<GameManager> 
{
    [Header("Victoria")]
    [SerializeField] int notasParaVictoria = 2;
    [SerializeField] GameObject victoryPanel;

    [Header("Derrota")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] float tiempoLimite = 30f;

    [Header("Escenas")]
    [SerializeField] string mainMenuScene = "MainMenu";

    bool gameEnded = false;
    public bool GameEnded => gameEnded;

    void Start()
    {
        if (victoryPanel != null) victoryPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        NotesManager.Instance.OnNoteCollected += CheckVictory;

        UserInterfaceManager.Instance.RegisterPanel(UserInterfaceManager.PanelType.Victory, () => victoryPanel?.SetActive(true));
        UserInterfaceManager.Instance.RegisterPanel(UserInterfaceManager.PanelType.GameOver, () => gameOverPanel?.SetActive(true));
    }

    void Update()
    {
        if (!gameEnded)
        {
            tiempoLimite -= Time.deltaTime;

            if (tiempoLimite <= 0)
            {
                TriggerGameOver();
            }
        }
    }

    void CheckVictory()
    {
        if (!gameEnded && NotesManager.Instance.NoteCount >= notasParaVictoria)
            TriggerVictory();
    }

    public void TriggerVictory()
    {
        if (gameEnded) return;
        gameEnded = true;

        if (victoryPanel != null) victoryPanel.SetActive(true);

        UserInterfaceManager.Instance.RequestOpenPanel(UserInterfaceManager.PanelType.Victory);
    }

    public void TriggerGameOver()
    {
        if (gameEnded) return;
        gameEnded = true;

        if (gameOverPanel != null) gameOverPanel.SetActive(true);

        UserInterfaceManager.Instance.RequestOpenPanel(UserInterfaceManager.PanelType.GameOver);
    }

    public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    public void GoToMainMenu() => SceneManager.LoadScene(mainMenuScene);
}