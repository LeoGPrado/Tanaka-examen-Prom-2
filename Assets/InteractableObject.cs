using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{

    [SerializeField] private UnityEvent onInteract;
    [SerializeField] private string interactText = "Presiona E";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInteract()
    {
        onInteract.Invoke();
    }
}
