using System;
using UnityEngine;

public class UserInterfaceManager : MonoBehaviour
{
    public static UserInterfaceManager Instance { get; private set; }
    public enum PanelType { None, Inventory, Notes, Victory, GameOver }

    [Header("Player Reference")]
    [SerializeField] PlayerMovement movement;
    [SerializeField] PlayerCamera cam;

    public PanelType ActivePanel { get; private set; } = PanelType.None;
    PanelType pendingPanel = PanelType.None;

    Action openInventory, openNotes, openVictory, openGameOver;

    public bool IsAnyPanelOpen() => ActivePanel != PanelType.None;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }
    }

    public void RegisterPanel(PanelType type, Action callback)
    {
        switch (type)
        {
            case PanelType.Inventory: openInventory = callback; break;
            case PanelType.Notes: openNotes = callback; break;
            case PanelType.Victory: openVictory = callback; break;
            case PanelType.GameOver: openGameOver = callback; break;
        }
    }

    public bool RequestOpenPanel(PanelType type)
    {
        if (ActivePanel == type) return true;
        if (ActivePanel != PanelType.None) { pendingPanel = type; return false; }
        SetPanel(type);
        return true;
    }

    public void ReportClosedPanel(PanelType type)
    {
        if (ActivePanel != type) return;
        if (pendingPanel != PanelType.None)
        {
            PanelType next = pendingPanel;
            pendingPanel = PanelType.None;
            SetPanel(next);
            Trigger(next);
        }
        else SetPanel(PanelType.None);
    }

    void SetPanel(PanelType p)
    {
        ActivePanel = p;
        bool blocking = p != PanelType.None;
        if (cam != null) cam.LockCamera(blocking);
        if (movement != null) movement.CanMove(!blocking);
        Cursor.lockState = blocking ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = blocking;
    }

    void Trigger(PanelType type)
    {
        switch (type)
        {
            case PanelType.Inventory: openInventory?.Invoke(); break;
            case PanelType.Notes: openNotes?.Invoke(); break;
            case PanelType.Victory: openVictory?.Invoke(); break;
            case PanelType.GameOver: openGameOver?.Invoke(); break;
        }
    }
}
