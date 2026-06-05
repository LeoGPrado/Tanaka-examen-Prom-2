using UnityEngine;

public class DoorCodePuzzle : MonoBehaviour
{


    [SerializeField] private Transform door;
    [SerializeField] private RotateDoors rd;
    [SerializeField] private GameObject codePad;

    [SerializeField] private bool locked;
    [SerializeField] private bool opening;
    




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        codePad.SetActive(false);
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
        OpenCodePad();
    }

    private void OpenCodePad()
    {
        if (!locked) return;
        codePad.SetActive(true);
    }

    public void UnlockDoorWithCode()
    {
        locked = false;
        opening = true;
        codePad.SetActive(false);
    }
}
