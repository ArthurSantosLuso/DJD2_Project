using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AdmComputer : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject passwordPanel;
    [SerializeField] private GameObject desktopPanel;
    [SerializeField] private GameObject softwerePanel;
    [SerializeField] private GameObject textFilePanel;
    [SerializeField] private Button inputPasswordButton;
    [SerializeField] private Button administrationButton;

    [SerializeField] private float timePasswordButtonUnable;

    [SerializeField] private RegularComputer computerToBeUnlocked;

    [Header("Audio")]
    [SerializeField] private AudioClip accessDeniedAudio;
    [SerializeField] private AudioClip accessGrantedAudio;

    private void Start()
    {
        inputField.text = "";
    }

    public void CheckIfPasswordIsCorrect()
    {
        if (inputField.text == "319256")
        {
            AudioManager.Instance.PlaySound(accessGrantedAudio);
            Debug.Log("Acertou a password!");
            desktopPanel.SetActive(true);
            passwordPanel.SetActive(false);
        }
        else
        {
            AudioManager.Instance.PlaySound(accessDeniedAudio);
            Debug.Log("Errou a password!");
        }
        StartCoroutine(DisablePasswordButton(timePasswordButtonUnable));
    }

    public void UnlockRagularComputer()
    {
        computerToBeUnlocked.UnlockComputer();
        administrationButton.onClick = null;
    }

    public void OpenTextFile()
    {
        softwerePanel.SetActive(false);
        textFilePanel.SetActive(true);
    }

    public void CloseTextFile()
    {
        softwerePanel.SetActive(true);
        textFilePanel.SetActive(false);
    }

    private IEnumerator DisablePasswordButton(float seconds)
    {
        inputPasswordButton.enabled = false;
        
        yield return new WaitForSeconds(seconds);

        inputPasswordButton.enabled = true;
    }
}
