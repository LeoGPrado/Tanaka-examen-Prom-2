using Unity.VisualScripting;
using UnityEngine;

public class DoorKeyPuzzle : MonoBehaviour
{

    [SerializeField] private Transform pivot;
    [SerializeField] private bool locked;
    [SerializeField] private bool opening;

    [SerializeField] private float openSpeed = 2f;
    [SerializeField] private float openingAngle = 90f;
    private Quaternion closingRotation;
    private Quaternion openingRotation;

    public bool hasKey;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        locked = true;
        closingRotation = pivot.rotation;
        openingRotation = Quaternion.Euler(pivot.eulerAngles + new Vector3(0, openingAngle, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (opening)
        {
            pivot.rotation = Quaternion.Slerp(pivot.rotation, openingRotation, Time.deltaTime * openSpeed);
        }
    }

    public void OnInteract()
    {
        TryOpeningDoor();
    }

    public void TryOpeningDoor()
    {
        if (!locked || !hasKey) return;
        
        if (hasKey)
        {
            locked = false;
            opening = true;
        }
    }

    private void UnlockDoor()
    {
        locked = false;
        opening = true;
    }
}
