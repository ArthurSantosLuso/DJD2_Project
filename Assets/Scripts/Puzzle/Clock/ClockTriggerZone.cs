using UnityEngine;

public class ClockTriggerZone : MonoBehaviour
{
    [SerializeField] private ClockController clockController;
    [SerializeField] private string expectedHandTag;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(expectedHandTag))
        {
            Debug.Log($"Hand {other.tag} entrou, mas não pertence a esse collider");
            return;
        }

        Debug.Log($"Hand {other.tag} entrou, e pertence a esse collider");
        clockController.OnHandEnterZone(expectedHandTag);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(expectedHandTag))
        {
            Debug.Log($"Hand {other.tag} entrou, mas não pertence a esse collider");
            return;
        }

        clockController.OnHandExitZone(expectedHandTag);
    }
}
