using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float flySpeed = 8f;
    public float takeOffForce = 5f;        // Fuerza del salto de despegue
    public float takeOffDuration = 0.3f;  // Duración del impulso inicial
    public NewActions inputAction;
    public Transform cameraTransform;
    public float gravity = -9.8f;

    private CharacterController charCon;
    private Vector2 movement;
    private Vector3 velocityY;
    private Vector2 look;

    public float sensitivity = 0.5f;
    public float minLimit = -40f;
    public float maxLimit = 40f;
    public float currentRotationY;

    private bool isFlying = false;
    private bool isTakingOff = false;      // Estado de despegue
    private float takeOffTimer = 0f;

    private void Awake()
    {
        inputAction = new NewActions();
        charCon = GetComponent <CharacterController>();

        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Start()
    {
        inputAction.Player.Enable();

        inputAction.Player.Move.performed += SetMovement;
        inputAction.Player.Move.canceled += obj => movement = Vector2.zero;

        inputAction.Player.Look.performed += SetLook;
        inputAction.Player.Look.canceled += obj => look = Vector2.zero;

        inputAction.Player.Fly.performed += ToggleFly;
    }

    private void OnDisable()
    {
        inputAction.Player.Move.performed -= SetMovement;
        inputAction.Player.Move.canceled -= obj => movement = Vector2.zero;
        inputAction.Player.Look.performed -= SetLook;
        inputAction.Player.Look.canceled -= obj => look = Vector2.zero;
        inputAction.Player.Fly.performed -= ToggleFly;
        inputAction.Player.Disable();
    }

    private void SetMovement(InputAction.CallbackContext obj)
    {
        movement = obj.ReadValue<Vector2>();
    }

    private void SetLook(InputAction.CallbackContext obj)
    {
        look = obj.ReadValue<Vector2>();
    }

    private void ToggleFly(InputAction.CallbackContext obj)
    {
        if (!isFlying && charCon.isGrounded)
        {
            // Activar vuelo con impulso de despegue
            isFlying = true;
            isTakingOff = true;
            takeOffTimer = takeOffDuration;
            velocityY.y = takeOffForce;
        }
        else if (isFlying)
        {
            isFlying = false;
            isTakingOff = false;
        }
    }

    private void Update()
    {
        Movement();
        Look();
    }

    private void Look()
    {
        Vector2 mouseNormalized = look * sensitivity;
        currentRotationY = Mathf.Clamp(currentRotationY - mouseNormalized.y, minLimit, maxLimit);
        cameraTransform.localRotation = Quaternion.Euler(currentRotationY, 0, 0);
        transform.Rotate(Vector3.up * mouseNormalized.x);
    }

    private void Movement()
    {
        if (isFlying)
        {
            // Fase de despegue (impulso inicial)
            if (isTakingOff)
            {
                takeOffTimer -= Time.deltaTime;

                // Aplicar impulso hacia arriba mientras dura el despegue
                if (takeOffTimer > 0)
                {
                    Vector3 takeOffMovement = Vector3.up * velocityY.y * Time.deltaTime;
                    charCon.Move(takeOffMovement);
                }
                else
                {
                    isTakingOff = false;
                    velocityY.y = 0f;
                }
            }
            else
            {
                // VUELO NORMAL: Movimiento relativo a la cámara
                Vector3 forward = cameraTransform.forward;
                Vector3 right = cameraTransform.right;

                Vector3 flyDirection = (right * movement.x + forward * movement.y).normalized;
                charCon.Move(flyDirection * flySpeed * Time.deltaTime);

                // Desactivar vuelo al tocar suelo
                if (charCon.isGrounded)
                {
                    isFlying = false;
                    velocityY.y = -2f;
                }
            }
        }
        else
        {
            // CAMINATA NORMAL
            Vector3 move = transform.right * movement.x + transform.forward * movement.y;
            charCon.Move(move * speed * Time.deltaTime);

            // Gravedad
            if (charCon.isGrounded && velocityY.y < 0)
            {
                velocityY.y = -2f;
            }
            velocityY.y += gravity * Time.deltaTime;
            charCon.Move(velocityY * Time.deltaTime);
        }
    }
}