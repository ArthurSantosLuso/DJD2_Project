using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public class InitialElevatorScene : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private AudioClip elevatorSound;
    [SerializeField] private AudioClip elevatorCloseSound;
    [SerializeField] private GameObject endGameTrigger;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("Awake");
    }

    private void PlayInicialScene()
    {
        AudioManager.Instance.PlaySound(elevatorSound);
        SystemManager.Instance.PauseGame(SystemManager.PauseType.InitialScene);
    }

    private void OnTriggerEnter(Collider other)
    {
        FirstPersonMovement movement = other.GetComponent<FirstPersonMovement>();
        if (movement != null)
        {
            StartCoroutine(CloseDoor(movement));
        }
    }

    private IEnumerator CloseDoor(FirstPersonMovement movement)
    {
        movement.enabled = false;

        animator.SetTrigger("Close");
        AudioManager.Instance.PlaySound(elevatorCloseSound);

        yield return new WaitForSeconds(1f);

        movement.enabled = true;
        SystemManager.Instance.UnpauseGame();
        endGameTrigger.SetActive(true);
        enabled = false;
    }
}
