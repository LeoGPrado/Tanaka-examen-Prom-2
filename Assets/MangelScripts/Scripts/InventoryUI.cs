using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    const int MAX_SLOTS = 3;

    [Header("Panel")]
    [SerializeField] KeyCode toggleKey = KeyCode.Tab;
    [SerializeField] GameObject inventoryPanel;

    [Header("Item Info")]
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemDescriptionText;

    [Header("Slots — asignar en orden 0,1,2")]
    [SerializeField] Image[] icons = new Image[MAX_SLOTS];
    [SerializeField] Image[] backgrounds = new Image[MAX_SLOTS];

    bool isOpen = false;

    void Awake()
    {
        inventoryPanel.SetActive(false);
        for (int i = 0; i < MAX_SLOTS; i++)
            if (icons[i] != null) { icons[i].enabled = false; BindSlot(i); }
    }

    void Start()
    {
        InventoryManager.Instance.OnInventoryChanged += Refresh;
        UserInterfaceManager.Instance.RegisterPanel(UserInterfaceManager.PanelType.Inventory, () => Open(true));
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey)) Open(!isOpen);
    }

    void BindSlot(int i)
    {
        if (!icons[i].gameObject.TryGetComponent<EventTrigger>(out var t))
            t = icons[i].gameObject.AddComponent<EventTrigger>();
        Add(t, EventTriggerType.PointerEnter, _ => ShowInfo(i));
        Add(t, EventTriggerType.PointerExit, _ => ClearInfo());
    }

    void Add(EventTrigger t, EventTriggerType type, UnityEngine.Events.UnityAction<BaseEventData> cb)
    {
        var e = new EventTrigger.Entry { eventID = type };
        e.callback.AddListener(cb);
        t.triggers.Add(e);
    }

    void Open(bool state)
    {
        if (state)
        {
            if (!UserInterfaceManager.Instance.RequestOpenPanel(UserInterfaceManager.PanelType.Inventory)) return;
        }
        else UserInterfaceManager.Instance.ReportClosedPanel(UserInterfaceManager.PanelType.Inventory);

        isOpen = state;
        inventoryPanel.SetActive(isOpen);
        if (isOpen) Refresh(); else ClearInfo();
    }

    void Refresh()
    {
        for (int i = 0; i < MAX_SLOTS; i++)
        {
            ItemData item = InventoryManager.Instance.GetItem(i);
            bool has = item != null;
            icons[i].sprite = has ? item.sprite : null;
            icons[i].enabled = has;
            icons[i].raycastTarget = has;
            backgrounds[i].color = Color.white;
        }
    }

    void ShowInfo(int i)
    {
        ItemData item = InventoryManager.Instance.GetItem(i);
        if (item != null)
        {
            if (itemNameText != null) itemNameText.text = item.itemName;
            if (itemDescriptionText != null) itemDescriptionText.text = item.description;
        }
        else ClearInfo();
    }

    void ClearInfo()
    {
        if (itemNameText != null) itemNameText.text = "--";
        if (itemDescriptionText != null) itemDescriptionText.text = "";
    }

    void OnDestroy() => InventoryManager.Instance.OnInventoryChanged -= Refresh;
}