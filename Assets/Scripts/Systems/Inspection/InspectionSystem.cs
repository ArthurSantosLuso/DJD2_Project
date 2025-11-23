using TreeEditor;
using UnityEngine;

public class InspectionSystem : MonoBehaviour
{
    [Header("Inspection Settings")]
    [SerializeField] private float      rotationSpeed = 100.0f;
    [SerializeField] private GameObject firstPlane;

    private Transform   objectClone;
    private GameObject  originalObject;
    private Vector3     previousMousePosition;
    private bool        isInspecting = false;

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
        SystemManager.Instance.PauseGame(true);
    }

    private void StopInspection()
    {
        isInspecting = false;

        SystemManager.Instance.UnpauseGame();

        firstPlane.SetActive(false);

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

        GameObject inspecPrefab = target.GetComponent<Interactive>().interactiveData.inspectionPrefab;

        objectClone = Instantiate(inspecPrefab, firstPlane.transform).transform;
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
        }
    }
}