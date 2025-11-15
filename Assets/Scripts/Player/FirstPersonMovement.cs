using UnityEditor.Timeline;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonMovement : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float forwardSpeed = 3.0f;
    [SerializeField] private float strafeSpeed = 3.0f;
    [SerializeField] private float backwardSpeed = 3.0f;
    [SerializeField] private float verticalVelocity = 0f;

    [Header("Look Sentting")]
    [SerializeField] private float maxLookUpRange= 290.0f;
    [SerializeField] private float maxLookDownRange = 75.0f;

    private Transform head;
    private Vector3 headRotation;
    private CharacterController characterController;
    private Vector3 velocity;
    private Vector3 motion;


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
        HandleMovementInput();
        MovePlayer();
    }

    private void HandleMovementInput()
    {
        float forwardDir = Input.GetAxis("Forward");
        float strafeDir = Input.GetAxis("Strafe");

        if (forwardDir >= 0f)
            velocity.z = forwardDir * forwardSpeed;
        else
            velocity.z = forwardDir * backwardSpeed;

        velocity.x = strafeDir * strafeSpeed;
    }

    private void MovePlayer()
    {
        if (characterController.isGrounded)
            verticalVelocity = -1f;
        else
            verticalVelocity += Physics.gravity.y * Time.deltaTime;

        motion = velocity;
        motion = transform.TransformVector(motion);
        motion.y = verticalVelocity;
        
        characterController.Move(motion * Time.deltaTime);
    }

}
