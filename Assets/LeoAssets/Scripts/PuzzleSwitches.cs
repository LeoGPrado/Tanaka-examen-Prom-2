using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PuzzleSwitches : MonoBehaviour, IInteractable
{
    [Header("Settings")]
    public string puzzlePrompt = "[E] Interactuar";
    [SerializeField] GameObject puzzlePanel;
    [SerializeField] Toggle[] fuseButtons;
    [SerializeField] Scrollbar[] visualFuses;
    [SerializeField] Image[] feedbackLights;
    [SerializeField] Color lightColorOn = Color.green;
    [SerializeField] Color lightColorOff = Color.red;

    [Header("Evento")]
    [SerializeField] UnityEvent PuzzleSolved;

    public string PuzzlePrompt => puzzlePrompt;
    bool isSolved = false;

    void Start()
    {
        puzzlePanel.SetActive(false);

        for (int i = 0; i < fuseButtons.Length; i++)
        {
            int index = i;
            fuseButtons[i].onValueChanged.AddListener((val) => OnToggleChanged(index, val));
        }

        SetAllSwitchesOff();
    }

    public string GetTextInteract() => puzzlePrompt;
    public void Interact(Vector3 interactorPosition)
    {
        if (isSolved) return;

        TogglePanel(true);
    }

    private void SetAllSwitchesOff()
    {
        for (int i = 0; i < fuseButtons.Length; i++)
        {
            fuseButtons[i].SetIsOnWithoutNotify(false);
        }

        SyncAllVisuals();
    }

    public void TogglePanel(bool state)
    {
        puzzlePanel.SetActive(state);
    }

    private void OnToggleChanged(int index, bool value)
    {
        if (!value)
        {
            fuseButtons[index].SetIsOnWithoutNotify(true);
            return;
        }

        ApplySwitchRules(index);
        SyncAllVisuals();
        CheckWin();
    }

    private void ApplySwitchRules(int pressedIndex)
    {
        if (fuseButtons.Length < 4) return;

        switch (pressedIndex)
        {
            case 0:
                if (fuseButtons[2].isOn) fuseButtons[2].SetIsOnWithoutNotify(false);
                break;

            case 1:
                if (fuseButtons[0].isOn) fuseButtons[0].SetIsOnWithoutNotify(false);
                break;

            case 2:
                break;

            case 3:
                if (fuseButtons[1].isOn) fuseButtons[1].SetIsOnWithoutNotify(false);
                break;
        }
    }

    private void SyncAllVisuals()
    {
        Color colorOn = lightColorOn;
        colorOn.a = 1f;
        Color colorOff = lightColorOff;
        colorOff.a = 1f;

        for (int i = 0; i < fuseButtons.Length; i++)
        {
            bool state = fuseButtons[i].isOn;
            visualFuses[i].value = state ? 1f : 0f;
            feedbackLights[i].color = state ? colorOn : colorOff;
        }
    }

    private void CheckWin()
    {
        foreach (Toggle t in fuseButtons)
        {
            if (!t.isOn) return;
        }

        isSolved = true;
        print("Puzzle 2 Variante A resuelta");
        PuzzleSolved?.Invoke();
        puzzlePrompt = string.Empty;
        TogglePanel(false);
    }
}