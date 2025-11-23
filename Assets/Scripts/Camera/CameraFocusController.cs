using UnityEngine;

public class CameraFocusController : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float moveSpeed;

    private Transform cam;
    private Transform normalPosition;
    private Transform targetFocusPoint;
    private bool isFocusing = false;

    private void Start()
    {
        cam = playerCamera;

        normalPosition = new GameObject("CameraNormalPoint").transform;
        normalPosition.position = cam.position;
        normalPosition.rotation = cam.rotation;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isFocusing && targetFocusPoint != null)
        {
            cam.position = 
                Vector3.Lerp(cam.position, targetFocusPoint.position, moveSpeed * Time.deltaTime);
            cam.rotation =
                Quaternion.Lerp(cam.rotation, targetFocusPoint.rotation, moveSpeed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.E))
                ExitFocus();
        }
    }

    public void EnterFocus(Transform focusPoint)
    {
        targetFocusPoint = focusPoint;
        isFocusing = true;

        SystemManager.Instance.PauseGame(true);
    }

    public void ExitFocus()
    {
        isFocusing = false;
        targetFocusPoint = null;

        SystemManager.Instance.UnpauseGame();

        cam.position = normalPosition.position;
        cam.rotation = normalPosition.rotation;
    }
}
