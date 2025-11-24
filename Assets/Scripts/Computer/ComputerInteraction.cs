using TMPro;
using UnityEngine;

public class ComputerInteraction : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject passwordPanel;
    [SerializeField] private GameObject desktopPanel;

    private bool isPasswordCorrect = false;

    //private void OnEnable()
    //{
    //    if (isPasswordCorrect)
    //    {

    //    }
    //    else
    //    {
    //        desktopPanel.SetActive(false);
    //        passwordPanel.SetActive(true);
    //    }
    //}

    

    private void Start()
    {
        inputField.text = "";
    }

    public void CheckIfPasswordIsCorrect()
    {
        if (inputField.text == "123456")
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
}
