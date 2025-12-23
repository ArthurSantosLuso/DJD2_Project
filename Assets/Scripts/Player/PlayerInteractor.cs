using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private float      interactRange = 5f;
    [SerializeField] private bool       debugMode = false;
    [SerializeField] private UIManager  uiManager;

    private Transform   cameraTransform;
    private Interactive currentTarget;
    private bool        _refreshCurrentInteractive;

    private void Start()
    {
        cameraTransform             = Camera.main.transform;
        currentTarget               = null;
        _refreshCurrentInteractive  = false;
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
        else if (interactive != currentTarget || _refreshCurrentInteractive)
            SetCurrentTarget(interactive);
    }

    private void DetectInput()
    {
        if (Input.GetButtonDown("Interact") && currentTarget != null)
        {
            currentTarget.Interact();
            _refreshCurrentInteractive = true;
        }
    }

    private void SetCurrentTarget(Interactive newTarget)
    {
        currentTarget               = newTarget;
        _refreshCurrentInteractive  = false;

        string interactionMessage = newTarget.GetInteractionMessage();

        if (interactionMessage != null && interactionMessage.Length > 0)
        {
            SystemManager.Instance.ShowUI(interactionMessage, true);
        }
        else
        {
            SystemManager.Instance.HideUI();
        }
        currentTarget.ApplyFocus();
        if (debugMode) Debug.Log("Looking at an interactable");
    }

    private void ClearCurrentTarget()
    {
        currentTarget.LoseFocus();
        currentTarget = null;

        uiManager.ShowDefaultCrosshair();
        uiManager.HideInteractionPanel();

        if (debugMode) Debug.Log("Not Looking at an interactable");
    }

    public void RefreshCurrentInteractive()
    {
        _refreshCurrentInteractive = true;
    }
}
