using UnityEngine;

public class RotateDoors : MonoBehaviour
{

    [SerializeField] private Transform pivot;
    [SerializeField] private float speedRotation = 90f;
    [SerializeField] private float openedAngle = 90f;
    [SerializeField] private bool open = false;
    [SerializeField] private bool opening = false;
    [SerializeField] private bool closing = false;
    private Quaternion closingRotation;
    private Quaternion openingRotation;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        closingRotation = pivot.localRotation;
        openingRotation = closingRotation * Quaternion.Euler(0f, openedAngle, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (opening)
        {
            pivot.localRotation = Quaternion.RotateTowards(pivot.localRotation, openingRotation, speedRotation * Time.deltaTime);

            if (Quaternion.Angle(pivot.localRotation, openingRotation) < 0.1f)
            {
                pivot.localRotation = openingRotation;
                opening = false;
                open = true;
            }
        }
        else if (closing)
        {
            pivot.localRotation = Quaternion.RotateTowards(pivot.localRotation, closingRotation, speedRotation * Time.deltaTime);

            if(Quaternion.Angle(pivot.localRotation, closingRotation) < 0.1f)
            {
                pivot.localRotation = closingRotation;
                closing = false;
                open = false;
            }
        }
    }

    public void OnInteract()
    {
        if (opening || closing) return;

        if (open) closing = true;
        else opening = true;
    }
}
