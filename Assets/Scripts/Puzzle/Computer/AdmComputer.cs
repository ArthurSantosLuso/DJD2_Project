using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

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
    [SerializeField] private DrawerController drawerToBeUnlocked;

    [Header("Items to be removed when puzzle's over")]
    [SerializeField] private List<Interactive> itemsRemove;

    [Header("Audio")]
    [SerializeField] private AudioClip accessDeniedAudio;

    private void Start()
    {
        inputField.text = "";
    }

    public void CheckIfPasswordIsCorrect()
    {
        if (inputField.text == "319256")
        {
            drawerToBeUnlocked.UnlockDrawer();
            Debug.Log("Acertou a password!");
            RemoveItems();
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

    private void RemoveItems()
    {
        foreach (Interactive interactive in itemsRemove)
        {
            InteractionManager.instance.playerInventory.Remove(interactive);
        }
    }
}
