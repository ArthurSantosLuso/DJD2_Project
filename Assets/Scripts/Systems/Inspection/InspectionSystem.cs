using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class InspectionSystem : MonoBehaviour
{
    [Header("Inspection Settings")]
    [SerializeField] private float      rotationSpeed = 100.0f;
    [SerializeField] private GameObject firstPlane;
    [SerializeField] Volume volumeWithBlur;

    private DepthOfField blur;

    private Transform   objectClone;
    private GameObject  originalObject;
    private Vector3     previousMousePosition;
    private bool        isInspecting = false;
    private bool        isInspectingFromInventory = false;

    [Header("Zoom Settings")]
    [SerializeField] private float zoomSpeed = 2.0f;
    [SerializeField] private float zoomLerpSpeed = 10.0f;
    [SerializeField] private float minZoomZ = 0.3f;
    [SerializeField] private float maxZoomZ = 2.0f;

    private float targetZoomZ;
    private float originalPlaneZ;

    private void Update()
    {
        if (!isInspecting)
            return;

        if (Input.GetButtonDown("Inspect"))
        {
            StopInspection();
        }

        HandleObjectRotation();
        HandleZoom();
        UpdateZoomPosition();
    }

    private void StartInspection()
    {
        isInspecting = true;
        firstPlane.SetActive(true);
        SystemManager.Instance.PauseGame(SystemManager.PauseType.Inspection);
        if (volumeWithBlur.profile.TryGet(out blur))
        {
            blur.active = true;
        }

        originalPlaneZ = firstPlane.transform.localPosition.z;
        targetZoomZ = originalPlaneZ;
    }

    private void StopInspection()
    {
        isInspecting = false;

        SystemManager.Instance.UnpauseGame();

        Vector3 resetPos = firstPlane.transform.localPosition;
        resetPos.z = originalPlaneZ;
        firstPlane.transform.localPosition = resetPos;

        firstPlane.SetActive(false);

        if (objectClone != null)
            Destroy(objectClone.gameObject);

        objectClone = null;

        if (!isInspectingFromInventory)
            originalObject.SetActive(true);
        originalObject = null;

        if (volumeWithBlur.profile.TryGet(out blur))
        {
            blur.active = false;
        }
    }

    public void InspectObject(GameObject target, bool inspectionFromInventory)
    {
        if (isInspecting)
            StopInspection();

        isInspectingFromInventory = inspectionFromInventory;
        originalObject = target;
        StartInspection();

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

    private void UpdateZoomPosition()
    {
        Vector3 pos = firstPlane.transform.localPosition;

        pos.z = Mathf.Lerp(
            pos.z,
            targetZoomZ,
            zoomLerpSpeed * Time.deltaTime
        );

        firstPlane.transform.localPosition = pos;
    }

    private void HandleZoom()
    {
        float scroll = Input.mouseScrollDelta.y;

        if (scroll == 0)
            return;

        targetZoomZ -= scroll * zoomSpeed * Time.deltaTime;

        // limit the zoom to the max and min value
        targetZoomZ = Mathf.Clamp(targetZoomZ, minZoomZ, maxZoomZ);
    }


}