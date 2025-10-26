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
        DetectInteractable();
    }

    private void DetectInteractable()
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
                return;
            }
        }

        ClearCurrentTarget();
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
