using System;
using UnityEngine;

public class DrawerOpener : MonoBehaviour
{

    [SerializeField] private float speedOpen = 2f;
    [SerializeField] private float distanceOpening = 0.5f;

    private bool open = false;
    private bool opening = false;
    private bool closing = false;

    private Vector3 closedPos;
    private Vector3 openedPos;

    [SerializeField] private float openedDifPosition;


    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        closedPos = transform.localPosition;
        openedPos = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - distanceOpening);
    }

    // Update is called once per frame
    void Update()
    {
        if (opening)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, openedPos, Time.deltaTime * speedOpen);

            if (Vector3.Distance(transform.localPosition, openedPos) < 0.0001f)
            {
                transform.localPosition = openedPos;

                open = true;
                opening = false;
            }
        }
  

        if (closing)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, closedPos, Time.deltaTime * speedOpen);

            if (Vector3.Distance(transform.localPosition, closedPos) < 0.0001f)
            {
                transform.localPosition = closedPos;

                open = false;
                closing = false;
            }
        }

        openedDifPosition = closedPos.z - transform.position.z;
    }

    public void OnInteract()
    {
        OpenClose();
    }

    public void OpenClose()
    {
        if (opening || closing) return;

        if (!open)
        {
            opening = true;
            closing = false;
        }
        else
        {
            closing = true;
            opening = false;
        }
    }
}
