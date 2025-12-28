using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class RegularComputer : MonoBehaviour
{
    private enum PuzzleState { Beggining, OnGoing, End }

    [SerializeField] private GameObject desktopPanel;
    [SerializeField] private GameObject softwarePanel;
    [SerializeField] private GameObject passwordPanel;
    [SerializeField] private GameObject investmentPanel;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private DrawerController drawerToBeUnlocked;

    [Header("Buttons")]
    [SerializeField] private Button investmentButton;
    [SerializeField] private Button negativeButton;
    [SerializeField] private Button positiveButton;

    [Header("Audio")]
    [SerializeField] private AudioClip unlockAudio;
    [SerializeField] private PhoneCallData[] phoneAudios;

    private PuzzleState state;
    private bool[] answers;
    private int currentAudio = 0;


    public void UnlockComputer()
    {
        AudioManager.Instance.PlaySound(unlockAudio);
        passwordPanel.SetActive(false);
        desktopPanel.SetActive(true);
        answers = new bool[phoneAudios.Length];
    }

    public void StartPhonePuzzle()
    {
        if (state == PuzzleState.End) return;

        state = PuzzleState.Beggining;
        softwarePanel.SetActive(false);
        investmentPanel.SetActive(true);
    }

    public void OnButtonClick(bool isPositive)
    {
        if (state == PuzzleState.Beggining)
        {
            state = PuzzleState.OnGoing;
            
            negativeButton.gameObject.SetActive(true);
            positiveButton.GetComponentInChildren<TMP_Text>()
                .text = "Accept";
            StartCoroutine(PlayPhoneSound(phoneAudios[currentAudio].audio));
        }
        else
        {
            answers[currentAudio] = isPositive;
            if (currentAudio == phoneAudios.Length - 1)
            {
                if (CheckIfPuzzleIsCorrect())
                {
                    // Puzzle is over
                    state = PuzzleState.End;
                    softwarePanel.SetActive(true);
                    investmentPanel.SetActive(false);
                    drawerToBeUnlocked.UnlockDrawer();
                }
                else
                {
                    // Restart Puzzle
                    state = PuzzleState.Beggining;
                    currentAudio = 0;
                    titleText.text = "Wrong! You DID NOT pay attencion to the voices.";
                    negativeButton.gameObject.SetActive(false);
                    positiveButton.GetComponentInChildren<TMP_Text>().text = "Restart";
                }
            }
            else
            {
                currentAudio++;
                StartCoroutine(PlayPhoneSound(phoneAudios[currentAudio].audio));
            }
        }
    }

    private IEnumerator PlayPhoneSound(AudioClip audio, float timeBeforeStart = 0.5f)
    {
        titleText.text = "Listenning...";
        SystemManager.Instance.PauseGame(SystemManager.PauseType.PhonePause);
        positiveButton.enabled = false;
        negativeButton.enabled = false;

        yield return new WaitForSeconds(timeBeforeStart);

        AudioManager.Instance.PlaySound(audio);

        yield return new WaitForSeconds(audio.length + 1);
        SystemManager.Instance.UnpauseGame(SystemManager.PauseType.PhonePause);
        positiveButton.enabled = true;
        negativeButton.enabled = true;
        titleText.text = "What's your answer?";
    }

    private bool CheckIfPuzzleIsCorrect()
    {
        for (int i = 0; i < phoneAudios.Length; i++)
        {
            if (answers[i] != phoneAudios[i].isTrue)
                return false;
        }
        return true;
    }
}
