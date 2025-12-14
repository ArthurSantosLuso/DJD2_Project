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
    private bool        isInspectingFromInventory = false;

    // Remove it later
    [SerializeField] GameObject selfCardPanel;
    [SerializeField] GameObject coworkerCardPanel;

    private void Update()
    {
        if (!isInspecting)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            StopInspection();
        }

        HandleObjectRotation();
    }

    private void StartInspection()
    {
        isInspecting = true;
        firstPlane.SetActive(true);
        SystemManager.Instance.PauseGame(SystemManager.PauseType.Inspection);
    }

    private void StopInspection()
    {
        isInspecting = false;

        SystemManager.Instance.UnpauseGame();

        firstPlane.SetActive(false);

        // -----
        // Remove it later
        if (originalObject.name == "IDCardSelf")
            selfCardPanel.SetActive(false);
        else if (originalObject.name == "IDCardCoworker")
            coworkerCardPanel.SetActive(false);
        // -----
        if (objectClone != null)
            Destroy(objectClone.gameObject);

        objectClone = null;

        if (!isInspectingFromInventory)
            originalObject.SetActive(true);
        originalObject = null;
    }

    public void InspectObject(GameObject target, bool inspectionFromInventory)
    {
        if (isInspecting)
            StopInspection();

        isInspectingFromInventory = inspectionFromInventory;
        originalObject = target;
        StartInspection();

        

        // -----
        // Remove it later
        if (originalObject.name == "IDCardSelf")
            selfCardPanel.SetActive(true);
        else if (originalObject.name == "IDCardCoworker")
            coworkerCardPanel.SetActive(true);
        // -----

        GameObject inspecPrefab = target.GetComponent<Interactive>().interactiveData.inspectionPrefab;

        objectClone = Instantiate(inspecPrefab, firstPlane.transform).transform;
        originalObject.SetActive(false);


        objectClone.localPosition = Vector3.zero;
        objectClone.localRotation = Quaternion.identity;

        ApplyInspectionOverride(target, objectClone.gameObject);
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

    private void ApplyInspectionOverride(GameObject original, GameObject clone)
    {
        InspectableMaterialOverride overrideData =
            original.GetComponent<InspectableMaterialOverride>();

        if (overrideData == null)
            return;

        MeshRenderer cloneRenderer = clone.GetComponentInChildren<MeshRenderer>();
        if (cloneRenderer != null)
            cloneRenderer.material = overrideData.InspectionMaterial;
    }
}