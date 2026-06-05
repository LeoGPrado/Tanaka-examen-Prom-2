using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour, IInteractable
{
    [Header("Data")]
    public ItemData itemData;

    [SerializeField] bool isPickable = true;
    [SerializeField] string itemPrompt = "[E] Recoger {0}";

    [Header("Events")]
    [SerializeField] UnityEvent OnPicked;
    [SerializeField] UnityEvent OnUnpickable;

    public bool IsPickable { set => isPickable = value; }

    public string GetTextInteract()
    {
        string name = itemData != null ? itemData.itemName : gameObject.name;
        return string.Format(itemPrompt, name);
    }

    public void Interact(Transform interactorTransform) => PickUp();

    public void PickUp()
    {
        if (!isPickable)
        {
            OnUnpickable?.Invoke();
            return;
        }

        if (itemData == null)
        {
            return;
        }

        bool pickedUp = false;

        if (itemData.itemType == ItemData.ItemType.Notes)
        {
            if (itemData is NoteData note)
            {
                NotesManager.Instance.AddNote(note);
                pickedUp = true;
            }
        }
        else if (itemData.itemType == ItemData.ItemType.Interactable)
        {
            pickedUp = InventoryManager.Instance.AddItem(itemData);
        }

        if (pickedUp)
        {
            OnPicked?.Invoke();
            gameObject.SetActive(false);
        }
    }
}