using UnityEngine;

public class VFX : MonoBehaviour
{
    [SerializeField]
    private Animator bloodSplashAnimator;

    private void OnEnable()
    {
        bloodSplashAnimator.SetTrigger("Splash");
    }
}
