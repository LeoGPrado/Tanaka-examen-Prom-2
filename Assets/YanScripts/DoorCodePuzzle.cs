using UnityEngine;

public class DoorCodePuzzle : MonoBehaviour
{

    [SerializeField] private RotateDoors rt;
    [SerializeField] private Transform pivot;
    [SerializeField] private GameObject codePanel;


    [SerializeField] private bool locked;
    [SerializeField] private bool opening;





    void Start()
    {
        opening = false;
        locked = true;
        codePanel.SetActive(false);
      
    }

    void Update()
    {
        
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
        if(opening) rt.Action();
        codePanel.SetActive(false);
    }
}
