using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private UIManager  uiManager;
    [SerializeField] private float      interactRange = 5f;

    private Transform   cameraTransform;
    private Interactive currentTarget;
    private bool        _refreshCurrentInteractive;

    private void Start()
    {
        cameraTransform = GetComponentInChildren<Camera>().transform;
        currentTarget = null;
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
        else if (interactive != currentTarget || _refreshCurrentInteractive)
            SetCurrentTarget(interactive);
    }

    private void DetectInput()
    {
        if (Input.GetKeyDown(KeyCode.F) && currentTarget != null)
        {
            currentTarget.Interact();
            _refreshCurrentInteractive = true;
        }
    }

    private void SetCurrentTarget(Interactive newTarget)
    {
        currentTarget = newTarget;
        currentTarget.ApplyFocus();
        _refreshCurrentInteractive = false;

        string interactionMessage = newTarget.GetInteractionMessage();

        if (interactionMessage != null && interactionMessage.Length > 0)
        {
            uiManager.ShowInteractionCrosshair();
            uiManager.ShowInteractionPanel(interactionMessage);
        }
        else
        {
            uiManager.ShowDefaultCrosshair();
            uiManager.HideInteractionPanel();
        }
    }

    private void ClearCurrentTarget()
    {
        currentTarget.LoseFocus();
        currentTarget = null;
        uiManager.HideInteractionPanel();
        uiManager.ShowDefaultCrosshair();
    }

    public void RefreshCurrentInteractive()
    {
        _refreshCurrentInteractive = true;
    }
}
