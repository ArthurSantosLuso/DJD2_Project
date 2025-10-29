using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private float interactRange = 5f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private bool debugMode = false;

    private IInteractable currentTarget;

    private void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;
    }

    void Update()
    {
        RaycastHit? hit = DetectInteractable();
        if (hit.HasValue)
            DetectInput((RaycastHit)hit);
    }

    private RaycastHit? DetectInteractable()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (debugMode)
            Debug.DrawRay(ray.origin, ray.direction * interactRange, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, interactRange))
        {
            if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
            {
                if (interactable != currentTarget)
                {
                    ClearCurrentTarget();
                    SetCurrentTarget(interactable);
                }
                return hit;
            }
        }

        ClearCurrentTarget();
        return null;
    }

    private void DetectInput(RaycastHit hit)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            hit.collider.TryGetComponent<Interactable>(out var interactable);
            interactable.Interact();
        }
    }

    private void SetCurrentTarget(IInteractable newTarget)
    {
        currentTarget = newTarget;
        currentTarget.OnFocus();
        if (debugMode) Debug.Log("Looking at an interactable");
    }

    private void ClearCurrentTarget()
    {
        if (currentTarget != null)
        {
            currentTarget.OnLoseFocus();
            currentTarget = null;
            if (debugMode) Debug.Log("Not Looking at an interactable");
        }
    }
}
