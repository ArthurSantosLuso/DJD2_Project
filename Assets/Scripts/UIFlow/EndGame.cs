using UnityEngine;

public class EndGame : MonoBehaviour
{
    private OpenScene openScene;

    private void Start()
    {
        openScene = GetComponent<OpenScene>();
    }

    private void OnTriggerEnter(Collider other)
    {
        FirstPersonMovement movement = other.GetComponent<FirstPersonMovement>();
        if (movement != null)
        {
            SystemManager.Instance.PauseGame(SystemManager.PauseType.StopEverything);
            openScene.LoadScene();
        }
    }
}
