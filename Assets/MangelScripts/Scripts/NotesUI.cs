using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotesUI : MonoBehaviour
{
    [Header("Lista")]
    [SerializeField] Transform container;
    [SerializeField] GameObject notePrefab;
    [SerializeField] GameObject notesPanel;
    [SerializeField] KeyCode toggleKey = KeyCode.N;

    [Header("Detalle")]
    [SerializeField] TextMeshProUGUI noteNameText;
    [SerializeField] TextMeshProUGUI noteBodyText;
    [SerializeField] Button closeButton;

    readonly List<GameObject> uiItems = new();
    bool isOpen = false;

    void Awake()
    {
        if (notesPanel != null) notesPanel.SetActive(false);
        if (closeButton != null) closeButton.onClick.AddListener(() => Open(false));
    }

    void Start()
    {
        NotesManager.Instance.OnNoteCollected += Refresh;
        UserInterfaceManager.Instance.RegisterPanel(UserInterfaceManager.PanelType.Notes, () => Open(true));
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey)) Open(!isOpen);
    }

    void Open(bool state)
    {
        if (state)
        {
            if (!UserInterfaceManager.Instance.RequestOpenPanel(UserInterfaceManager.PanelType.Notes)) return;
        }
        else UserInterfaceManager.Instance.ReportClosedPanel(UserInterfaceManager.PanelType.Notes);

        isOpen = state;
        if (notesPanel != null) notesPanel.SetActive(isOpen);
        if (isOpen) Refresh();
    }

    void Refresh()
    {
        if (NotesManager.Instance == null) return;
        var notes = NotesManager.Instance.GetCollectedNotes();

        foreach (var go in uiItems) if (go != null) Destroy(go);
        uiItems.Clear();

        foreach (var note in notes)
        {
            NoteData captured = note;
            GameObject go = Instantiate(notePrefab, container);
            uiItems.Add(go);

            var label = go.GetComponentInChildren<TextMeshProUGUI>();
            if (label != null) label.text = string.IsNullOrEmpty(note.itemName) ? "???" : note.itemName;

            if (go.TryGetComponent<Button>(out var btn))
                btn.onClick.AddListener(() => ShowDetail(captured));
        }
    }

    void ShowDetail(NoteData note)
    {
        if (noteNameText != null) noteNameText.text = note.itemName;
        if (noteBodyText != null) noteBodyText.text = note.GetParsedDescription();
    }

    void OnDestroy() => NotesManager.Instance.OnNoteCollected -= Refresh;
}