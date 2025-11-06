using System.Globalization;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonMovement : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float forwardSpeed = 3.0f;
    [SerializeField] private float strafeSpeed = 3.0f;
    [SerializeField] private float backwardSpeed = 3.0f;

    [Header("Look Sentting")]
    [SerializeField] private float mouseSensitivity = 2.0f;
    [SerializeField] private float maxLookUpRange= 290.0f;
    [SerializeField] private float maxLookDownRange = 75.0f;

    private Transform head;
    private Vector3 headRotation;
    private CharacterController characterController;


    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
        head = GetComponentInChildren<Camera>().transform;
    }

    private void Update()
    {
        HandleRotation();
        UpdateHead();    
    }

    private void HandleRotation()
    {
        float mouseXRotation = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseXRotation, 0);
    }

    private void UpdateHead()
    {
        headRotation = head.localEulerAngles;

        headRotation.x -= Input.GetAxis("Mouse Y");

        if (headRotation.x > 180f)
            headRotation.x = Mathf.Max(maxLookUpRange, headRotation.x);
        else
            headRotation.x = Mathf.Min(maxLookDownRange, headRotation.x);

        head.localEulerAngles = headRotation;
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float verticalSpeed = Input.GetAxis("Forward") * forwardSpeed;
        float horizontalSpeed = Input.GetAxis("Strafe") * forwardSpeed;

        Vector3 speed = new Vector3(horizontalSpeed, 0, verticalSpeed);
        speed = transform.rotation * speed;

        characterController.SimpleMove(speed);
    }

}
