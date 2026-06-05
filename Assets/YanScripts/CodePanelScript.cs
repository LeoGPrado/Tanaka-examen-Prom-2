using System;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CodePanelScript : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI codeText;
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private string correctCode = "2107";
    [SerializeField] private DoorCodePuzzle door;
    

    private string currentCode = "";
    private int maxDigit = 4;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CleanCode();
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        CleanCode();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)) Digit("0");
        if (Input.GetKeyDown(KeyCode.Alpha1)) Digit("1");
        if (Input.GetKeyDown(KeyCode.Alpha2)) Digit("2");
        if (Input.GetKeyDown(KeyCode.Alpha3)) Digit("3");
        if (Input.GetKeyDown(KeyCode.Alpha4)) Digit("4");
        if (Input.GetKeyDown(KeyCode.Alpha5)) Digit("5");
        if (Input.GetKeyDown(KeyCode.Alpha6)) Digit("6");
        if (Input.GetKeyDown(KeyCode.Alpha7)) Digit("7");
        if (Input.GetKeyDown(KeyCode.Alpha8)) Digit("8");
        if (Input.GetKeyDown(KeyCode.Alpha9)) Digit("9");

        if (Input.GetKeyDown(KeyCode.Backspace)) Delete();

        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)) CheckCode();

        if (Input.GetKeyDown(KeyCode.Escape)) ClosePanel();
    }

    public void Digit(string digit)
    {
        if (currentCode.Length >= maxDigit) return;
        currentCode = currentCode + digit;
        ShowCode();
    }

    public void Delete()
    {
        if (currentCode.Length > 0)
        {
            currentCode = currentCode.Substring(0, currentCode.Length - 1);
        }
        ShowCode();
        feedbackText.text = "";
    }

    public void CheckCode()
    {
        if (currentCode.Length != maxDigit)
        {
            feedbackText.text = "Faltan numeros";
            return;
        }

        if (currentCode == "2107")
        {
            feedbackText.text = "Correcto";
            door.UnlockDoorWithCode();
        }
        else
        {
            feedbackText.text = "Incorrecto";
            currentCode = "";
            Invoke("ShowCode", 0.5f);
        }
    }

    public void ClosePanel()
    {
        this.gameObject.SetActive(false);
    }

    public void ShowCode()
    {
        codeText.text = currentCode;
        while(codeText.text.Length < maxDigit)
        {
            codeText.text = codeText.text + "-";
        }
    }

    private void CleanCode()
    {
        currentCode = "";
        codeText.text = "----";
    }



}
