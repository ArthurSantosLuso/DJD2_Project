using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenScene : MonoBehaviour
{
    [HideInInspector] public int sceneIndex;
    private AudioClip startGameAudio;

    private void Start()
    {
        startGameAudio = Instantiate(Resources.Load<AudioClip>("Audio/Menu/startGame"));
    }

    public void LoadScene()
    {
        if(sceneIndex == 1)
        {
            StartCoroutine(StartGame());
            return;
        }

        SceneManager.LoadScene(sceneIndex);
    }

    private IEnumerator StartGame()
    {
        AudioManager.Instance.PlaySound(startGameAudio);

        yield return new WaitForSeconds(startGameAudio.length);

        SceneManager.LoadScene(sceneIndex);
    }
}
