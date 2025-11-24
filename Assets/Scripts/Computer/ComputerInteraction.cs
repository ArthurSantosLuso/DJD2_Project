using TMPro;
using UnityEngine;

public class ComputerInteraction : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject passwordPanel;
    [SerializeField] private GameObject desktopPanel;
    [SerializeField] private GameObject softwerePanel;
    [SerializeField] private GameObject textFilePanel;

    private void Start()
    {
        inputField.text = "";
    }

    public void CheckIfPasswordIsCorrect()
    {
        if (inputField.text == "319256")
        {
            Debug.Log("Acertou a password!");
            desktopPanel.SetActive(true);
            passwordPanel.SetActive(false);
            //isPasswordCorrect = true;
        }
        else
        {
            Debug.Log("Errou a password!");
        }
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
}
