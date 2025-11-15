using TreeEditor;
using UnityEngine;

public class InspectionSystem : MonoBehaviour
{
    [Header("Inspection Settings")]
    [SerializeField] private float rotationSpeed = 100.0f;
    [SerializeField] private GameObject firstPlane;

    private FirstPersonMovement playerMovement;
    private Transform objectClone;
    private GameObject originalObject;
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
        {
            StopInspection();
            originalObject.SetActive(true);
            originalObject = null;
        }

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
        if (objectClone != null)
            Destroy(objectClone.gameObject);

        objectClone = null;

    }

    public void InspectObject(GameObject target)
    {
        if (isInspecting)
            StopInspection();

        originalObject = target;
        StartInspection();

        objectClone = Instantiate(target, firstPlane.transform).transform;
        originalObject.SetActive(false);


        objectClone.localPosition = Vector3.zero;
        objectClone.localRotation = Quaternion.identity;

    }

    private void HandleObjectRotation()
    {
        if (objectClone == null)
            return;

        if (Input.GetMouseButtonDown(0))
            previousMousePosition = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - previousMousePosition;

            float rotX = delta.y * rotationSpeed * Time.deltaTime;
            float rotY = -delta.x * rotationSpeed * Time.deltaTime;

            objectClone.Rotate(this.transform.up, rotY, Space.World);
            objectClone.Rotate(this.transform.right, rotX, Space.World);

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