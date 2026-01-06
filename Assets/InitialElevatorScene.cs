using System.Runtime.InteropServices;
using UnityEngine;

public class InitialElevatorScene : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private AudioClip elevatorSound;
    [SerializeField] private AudioClip elevatorOpenSound;
    
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

    private void PlayOpenSound()
    {
        AudioManager.Instance.PlaySound(elevatorOpenSound);    
    }
}
