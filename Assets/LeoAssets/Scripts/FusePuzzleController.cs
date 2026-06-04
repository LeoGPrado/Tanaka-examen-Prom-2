using UnityEngine;
using UnityEngine.Events;

public class FusePuzzleController : MonoBehaviour, IInteractable
{
    [Header("Configuración")]
    [SerializeField] private GameObject puzzlePanel;
    public string puzzlePrompt = "[E] Reparar Fusibles";
    [SerializeField] private int requiredFuses = 4;

    [Header("Event")]
    [SerializeField] private UnityEvent OnPuzzleSolved;

    private int currentCorrectFuses = 0;
    private bool isSolved = false;

    public bool IsSolved => isSolved;

    void Start()
    {
        puzzlePanel.SetActive(false);
    }

    public string GetTextInteract() => puzzlePrompt;

    public void Interact(Vector3 interactorPosition)
    {
        if (isSolved) return;

        OpenPuzzle();
    }

    private void OpenPuzzle()
    {
        puzzlePanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CheckPlacement()
    {
        if (isSolved) return;

        currentCorrectFuses++;

        if (currentCorrectFuses >= requiredFuses)
        {
            WinPuzzle();
        }
    }

    private void WinPuzzle()
    {
        isSolved = true;
        print("Puzzle 2 Variante B resuelta");
        OnPuzzleSolved?.Invoke();
        puzzlePanel.SetActive(false);
    }
}