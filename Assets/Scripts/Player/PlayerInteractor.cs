using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private float  interactRange = 5f;
    [SerializeField] private bool   debugMode = false;

    private Transform   cameraTransform;
    private Interactive currentTarget;
    private bool        _refreshCurrentInteractive;

    private void Start()
    {
        cameraTransform = GetComponentInChildren<Camera>().transform;
        _refreshCurrentInteractive = false;
    }

    void Update()
    {
        CheckForInteractive();
        DetectInput();
    }

    private void CheckForInteractive()
    {
        Ray ray = new Ray(cameraTransform.transform.position, cameraTransform.transform.forward);
        if (debugMode)
            Debug.DrawRay(ray.origin, ray.direction * interactRange, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, interactRange, LayerMask.NameToLayer("Interactable")))
            UpdateCurrentInteractive(hit.collider);
        else if (currentTarget != null)
            ClearCurrentTarget();
    }

    private void UpdateCurrentInteractive(Collider collider)
    {
        Interactive interactive = collider.GetComponent<Interactive>();

        if (interactive == null || !interactive.isOn)
        {
            if (currentTarget != null)
                ClearCurrentTarget();
        }
        else if (interactive != currentTarget)
            SetCurrentTarget(interactive);
    }

    private void DetectInput()
    {
        if (Input.GetKeyDown(KeyCode.F) && currentTarget != null)
        {
            currentTarget.Interact();
        }
    }

    private void SetCurrentTarget(Interactive newTarget)
    {
        currentTarget = newTarget;
        currentTarget.ApplyFocus();
        if (debugMode) Debug.Log("Looking at an interactable");
    }

    private void ClearCurrentTarget()
    {
        currentTarget.LoseFocus();
        currentTarget = null;
        if (debugMode) Debug.Log("Not Looking at an interactable");
    }

    public void RefreshCurrentInteractive()
    {
        _refreshCurrentInteractive = true;
    }
}
