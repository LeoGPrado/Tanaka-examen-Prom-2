using Unity.VisualScripting;
using UnityEngine;

public class DoorKeyPuzzle : MonoBehaviour
{

    [SerializeField] private Transform door;
    [SerializeField] private RotateDoors rd;
    [SerializeField] private bool locked;
    [SerializeField] private bool opening;
    public bool hasKey;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        locked = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (opening)
        {
            rd.OnInteract();
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
