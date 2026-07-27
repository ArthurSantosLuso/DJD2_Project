using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class DrawerController : MonoBehaviour
{
    [SerializeField] private bool isLocked;
    [SerializeField] private bool isOpen;
    [SerializeField] private GameObject VFX;

    private AudioClip   openAudio;
    private AudioClip   closeAudio;
    private AudioClip   lockedAudio;
    private AudioClip   unlockAudio;

    private bool isPlayingLockedAudio = false;

    public bool IsLocked
    { 
        get
        {
            return isLocked;
        }
    }

    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isLocked", isLocked);
        isOpen = false;

        openAudio = Resources.Load<AudioClip>("Audio/Drawer/drawer_open");
        closeAudio = Resources.Load<AudioClip>("Audio/Drawer/drawer_close");
        lockedAudio = Resources.Load<AudioClip>("Audio/Drawer/drawer_locked");
        unlockAudio = Resources.Load<AudioClip>("Audio/Drawer/drawer_unlock");

    }

    public void UnlockDrawer()
    {
        VFX?.SetActive(false);
        isLocked = false;
        animator.SetBool("isLocked", isLocked);
        AudioManager.Instance.PlaySound(unlockAudio);
    }

    private void ChangeState()
    {
        isOpen = !isOpen;
        PlayOpenCloseAudio();
    }

    private void PlayOpenCloseAudio()
    {
        if (isOpen)
            AudioManager.Instance.PlaySound(openAudio);
        else
            AudioManager.Instance.PlaySound(closeAudio);
    }

    public void TryPlayLockedSound()
    {
        if (!isPlayingLockedAudio)
        {
            StartCoroutine(PlayLockedSound());
        }
    }

    private IEnumerator PlayLockedSound()
    {
        isPlayingLockedAudio = true;

        AudioManager.Instance.PlaySound(lockedAudio);

        yield return new WaitForSeconds(lockedAudio.length + 0.5f);

        isPlayingLockedAudio = false;
    }
}
