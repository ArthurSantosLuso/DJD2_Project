using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CameraFocusController : MonoBehaviour
{
    [SerializeField] private Transform  playerCamera;
    [SerializeField] private float      moveSpeed;

    private Transform   cam;
    private Transform   normalPosition;
    private Transform   targetFocusPoint;
    private bool        isFocusing     = false;
    private bool        isReturning    = false;

    public bool IsFocusing => isFocusing;

    private void Start()
    {
        cam = transform;

        normalPosition = new GameObject("CameraNormalPoint").transform;
        normalPosition.SetParent(cam.parent);
        normalPosition.localPosition = cam.localPosition;
        normalPosition.localRotation = cam.localRotation;
    }

    private void Update()
    {
        if (isFocusing && targetFocusPoint != null)
        {
            cam.position = 
                Vector3.Lerp(cam.position, targetFocusPoint.position, moveSpeed * Time.deltaTime);
            cam.rotation =
                Quaternion.Lerp(cam.rotation, targetFocusPoint.rotation, moveSpeed * Time.deltaTime);

            SystemManager.Instance.HideUI();
        }

        if (isReturning)
        {
            cam.position =
                Vector3.Lerp(cam.position, normalPosition.position, moveSpeed * Time.deltaTime);

            cam.rotation =
                Quaternion.Lerp(cam.rotation, normalPosition.rotation, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(cam.position, normalPosition.position) < 0.01f)
            {
                isReturning = false;

                cam.localPosition = normalPosition.localPosition;
                cam.localRotation = normalPosition.localRotation;
            }
        }
    }

    public void EnterFocus(Transform focusPoint)
    {
        isFocusing = true;
        isReturning = false;
        targetFocusPoint = focusPoint;

        SystemManager.Instance.HideUI();
        SystemManager.Instance.PauseGame(SystemManager.PauseType.FocusObject);
    }

    public void ExitFocus()
    {
        isFocusing = false;
        isReturning = true;
        targetFocusPoint = null;

        SystemManager.Instance.UnpauseGame();
    }
}
