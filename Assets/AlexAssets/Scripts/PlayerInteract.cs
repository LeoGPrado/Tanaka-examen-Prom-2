using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] float interactRange = 1f;
    [SerializeField] LayerMask interactLayer, obstructLayer;
    [SerializeField] Transform interactPoint;
    IInteractable currentInteractable;
    void Update()
    { 
        InteractDetector();
        InteractInput();
    }
    void InteractDetector()
    {
        float effectiveRange = interactRange;
        if (Physics.Raycast(interactPoint.position, interactPoint.forward, out RaycastHit obstructionHit, interactRange, obstructLayer))
        {
            effectiveRange = obstructionHit.distance + 0.01f;
        }
        if (Physics.Raycast(interactPoint.position, interactPoint.forward, out RaycastHit hit, Mathf.Min(effectiveRange, interactRange), interactLayer))
        {
           IInteractable interactable = hit.collider.GetComponentInParent<IInteractable>();
           if (interactable != null)
            {
                SetCurrentInteractable(interactable);
                return;
            }
        }
        ClearInteractable();
    }
    void InteractInput()
    {
        if (currentInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable.Interact(transform);
        }
    }
    void SetCurrentInteractable(IInteractable interactable)
    {
        currentInteractable = interactable;
    }
    void ClearInteractable()
    {
        currentInteractable = null;
    }
}
