using UnityEngine;

public class ElevatorSFXController : MonoBehaviour
{
    [SerializeField] private AudioClip open;
    [SerializeField] private AudioClip close;

    private void PlayOpenSound() => AudioManager.Instance.PlaySound(open);

    private void PlayCloseSound() => AudioManager.Instance.PlaySound(close);
}
