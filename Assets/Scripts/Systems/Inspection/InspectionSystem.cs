using TreeEditor;
using UnityEngine;

public class InspectionSystem : MonoBehaviour
{
    [Header("Inspection Settings")]
    [SerializeField] private float rotationSpeed = 100.0f;
    [SerializeField] private GameObject firstPlane;

    private FirstPersonMovement playerMovement;
    private Transform objectToInspect;
    private Vector3 previousMousePosition;
    private bool isInspecting = false;

    private void Awake()
    {
        playerMovement = GetComponentInParent<FirstPersonMovement>();
    }

    private void Update()
    {
        if (!isInspecting)
            return;

        if (Input.GetKeyDown(KeyCode.E))
            StopInspection();

        HandleObjectRotation();
    }

    private void StartInspection()
    {
        isInspecting = true;
        firstPlane.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        playerMovement.enabled = false;
    }

    private void StopInspection()
    {
        isInspecting = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        playerMovement.enabled = true;
        firstPlane.SetActive(false);

        // Destroy old inspected clone
        if (objectToInspect != null)
            Destroy(objectToInspect.gameObject);

        objectToInspect = null;
    }

    public void InspectObject(GameObject target)
    {
        if (isInspecting)
            StopInspection();


        StartInspection();

        objectToInspect = Instantiate(target, firstPlane.transform).transform;

        objectToInspect.localPosition = Vector3.zero;
        objectToInspect.localRotation = Quaternion.identity;

    }

    private void HandleObjectRotation()
    {
        if (objectToInspect == null)
            return;

        if (Input.GetMouseButtonDown(0))
            previousMousePosition = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - previousMousePosition;

            float rotX = delta.y * rotationSpeed * Time.deltaTime;
            float rotY = -delta.x * rotationSpeed * Time.deltaTime;

            objectToInspect.Rotate(this.transform.up, rotY, Space.World);
            objectToInspect.Rotate(this.transform.right, rotX, Space.World);

            previousMousePosition = Input.mousePosition;

            //Vector3 deltaMousePostion = Input.mousePosition - previousMousePosition;
            //float rotationX = deltaMousePostion.y * rotationSpeed * Time.deltaTime;
            //float rotationY = deltaMousePostion.x * rotationSpeed * Time.deltaTime;
            //Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
            //objectToInspect.rotation = rotation * objectToInspect.rotation;
            //previousMousePosition = Input.mousePosition;
        }
    }
}