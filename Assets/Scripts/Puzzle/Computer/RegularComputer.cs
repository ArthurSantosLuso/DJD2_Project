using UnityEditor;
using UnityEngine;

public class RegularComputer : MonoBehaviour
{
    [SerializeField] private GameObject desktopPanel;
    [SerializeField] private GameObject passwordPanel;
    
    [Header("Audio")]
    [SerializeField] private AudioClip unlockAudio;
    
    
    public void UnlockComputer()
    {
        AudioManager.Instance.PlaySound(unlockAudio);
        passwordPanel.SetActive(false);
        desktopPanel.SetActive(true);
    }
}
