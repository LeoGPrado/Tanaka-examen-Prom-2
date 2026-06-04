using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(RectTransform))]
public class DraggableFusePuzzle : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Configuración")]
    [SerializeField] private RectTransform correctTargetSlot;
    [SerializeField] private float requiredSnapDistance = 50f;
    [SerializeField] private float dragScaleMultiplier = 1.15f;

    private RectTransform fuseRectTransform;
    private CanvasGroup fuseCanvasGroup;
    private Canvas rootCanvasContainer;

    private Vector3 initialLocalPosition;
    private Transform initialParentTransform;
    private Vector3 initialScale;

    private bool isSnappedAndLocked = false;
    private FusePuzzleController mainPuzzleController;

    private void Awake()
    {
        fuseRectTransform = GetComponent<RectTransform>();
        fuseCanvasGroup = GetComponent<CanvasGroup>();
        initialScale = transform.localScale;
    }

    private void Start()
    {
        initialParentTransform = transform.parent;
        initialLocalPosition = transform.localPosition;
        rootCanvasContainer = GetComponentInParent<Canvas>();
        mainPuzzleController = FindFirstObjectByType<FusePuzzleController>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isSnappedAndLocked) return;

        fuseCanvasGroup.blocksRaycasts = false;
        fuseCanvasGroup.alpha = 0.7f;
        transform.localScale = initialScale * dragScaleMultiplier;
        transform.SetParent(rootCanvasContainer.transform, true);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isSnappedAndLocked) return;

        if (rootCanvasContainer.renderMode != RenderMode.ScreenSpaceOverlay)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(
                fuseRectTransform, eventData.position, rootCanvasContainer.worldCamera, out Vector3 worldPosition);
            fuseRectTransform.position = worldPosition;
        }
        else
        {
            fuseRectTransform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isSnappedAndLocked) return;

        fuseCanvasGroup.blocksRaycasts = true;
        fuseCanvasGroup.alpha = 1f;
        transform.localScale = initialScale;

        float distanceToTargetSlot = Vector2.Distance(fuseRectTransform.position, correctTargetSlot.position);

        if (distanceToTargetSlot <= requiredSnapDistance)
        {
            SnapToTargetSlot();
            return;
        }

        ResetToInitialPosition();
    }

    private void SnapToTargetSlot()
    {
        isSnappedAndLocked = true;
        transform.SetParent(correctTargetSlot, false);
        fuseRectTransform.localPosition = Vector3.zero;

        mainPuzzleController.CheckPlacement();
    }

    public void ResetToInitialPosition()
    {
        isSnappedAndLocked = false;
        transform.SetParent(initialParentTransform);
        transform.localPosition = initialLocalPosition;
    }
}