using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle_Sequence : MonoBehaviour
{
    [SerializeField] Button[] numpadButtons;
    [SerializeField] Image[] sequenceLights;

    [Header("Settings")]
    [SerializeField] Color lightOffColor = Color.black;
    [SerializeField] Color lightCorrectColor = Color.green;
    [SerializeField] Color lightErrorColor = Color.red;
    [SerializeField] UnityEngine.Events.UnityEvent OnSolved;
    private int currentSequenceStep = 0; 
    private int expectedNextValue = 0;   
    private bool isLockedOut = false;    
    void Start()
    {
        InitializePuzzle();
    }

    private void InitializePuzzle()
    {
        for (int i = 0; i < numpadButtons.Length; i++)
        {
            int buttonValue = i + 1; 
            Button btn = numpadButtons[i];

            if (btn != null)
            {
                btn.onClick.AddListener(() => OnButtonPressed(buttonValue));
            }
        }

        ResetLights();
    }

    private void OnButtonPressed(int pressedValue)
    {
        if (isLockedOut || currentSequenceStep >= 9) return;

        if (currentSequenceStep == 0 || pressedValue == expectedNextValue)
        {
            ProcessCorrectPress(pressedValue);
        }
        else
        {
            ProcessError();
        }
    }

    private void ProcessCorrectPress(int pressedValue)
    {
        if (currentSequenceStep < sequenceLights.Length)
        {
            sequenceLights[currentSequenceStep].color = lightCorrectColor;
        }

        currentSequenceStep++;

        expectedNextValue = (pressedValue % 9) + 1;
        if (currentSequenceStep >= 9)
        {
            CompletePuzzle();
        }
    }

    private void ProcessError()
    {
        StartCoroutine(ErrorResetRoutine());
    }

    private IEnumerator ErrorResetRoutine()
    {
        isLockedOut = true; 

        if (currentSequenceStep < sequenceLights.Length)
        {
            sequenceLights[currentSequenceStep].color = lightErrorColor;
        }
        WaitForSeconds waitForSeconds = new(0.5f);
        yield return waitForSeconds;

        currentSequenceStep = 0;
        expectedNextValue = 0;
        ResetLights();

        isLockedOut = false;
    }

    private void ResetLights()
    {
        foreach (Image light in sequenceLights)
        {
            if (light != null) light.color = lightOffColor;
        }
    }

    private void CompletePuzzle()
    {
        Debug.Log("[Puzzle Caja Fuerte] Secuencia completada. Abriendo compartimento...");
        OnSolved?.Invoke();
    }
}
