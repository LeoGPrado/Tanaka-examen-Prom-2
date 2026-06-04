using Unity.VisualScripting;
using UnityEngine;

public class LightActivator : MonoBehaviour
{

    [SerializeField] private GameObject lightContainer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateLights()
    {
        lightContainer.SetActive(true);
    }
}
