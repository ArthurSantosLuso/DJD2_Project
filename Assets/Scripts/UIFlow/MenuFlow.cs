using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFlow : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void LoadScene() => SceneManager.LoadScene(sceneName);

    public void CloseGame() => Application.Quit();
}
