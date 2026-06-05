using Unity.VisualScripting;
using UnityEngine;

public class DoorKeyPuzzle : MonoBehaviour
{
    [SerializeField] private RotateDoors rt;
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
            UnlockDoor();
        }
    }

    private void UnlockDoor()
    {
        opening = true;
        if(opening) rt.Action();
    }
}
