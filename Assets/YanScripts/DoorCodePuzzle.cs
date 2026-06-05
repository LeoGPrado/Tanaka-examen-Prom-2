using UnityEngine;

public class DoorCodePuzzle : MonoBehaviour
{

    private RotateDoors rt;
    [SerializeField] private Transform pivot;
    [SerializeField] private GameObject codePanel;

    [SerializeField] private float openSpeed = 2f;
    [SerializeField] private float openingAngle = 90f; 

    [SerializeField] private bool locked;
    [SerializeField] private bool opening;
    private Quaternion closingRotation;
    private Quaternion openingRotation;
    




    void Start()
    {
        locked = true;
        codePanel.SetActive(false);
        closingRotation = pivot.rotation;
        openingRotation = Quaternion.Euler(pivot.eulerAngles + new Vector3(0, openingAngle, 0));
    }

    void Update()
    {
        if (opening)
        {
            pivot.rotation = Quaternion.Slerp(pivot.rotation, openingRotation, Time.deltaTime * openSpeed);
            rt.Action();
        }
    }

    public void OnInteract()
    {
        OpenCodePanel();
    }

    public void OpenCodePanel()
    {
        if (!locked) return;
        codePanel.SetActive(true);
    }

    public void UnlockDoorWithCode()
    {
        locked = false;
        opening = true;

        codePanel.SetActive(false);
    }
}
