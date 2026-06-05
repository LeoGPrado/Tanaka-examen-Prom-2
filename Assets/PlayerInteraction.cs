using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    [SerializeField] private float range = 3f;
    [SerializeField] private LayerMask layermsk;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, range, layermsk))
            {
                hit.collider.BroadcastMessage("OnInteract", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
