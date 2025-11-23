using TMPro;
using UnityEngine;

public class ComputerInteraction : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    private void Start()
    {
        inputField.text = "";
        inputField.onEndEdit.AddListener(TextoFinalizado);
    }

    public void ShowText()
    {
        string text = inputField.text;
        Debug.Log(text);
    }

    void TextoFinalizado(string textoDigitado)
    {
        Debug.Log("O jogador terminou de digitar: " + textoDigitado);
    }
}
