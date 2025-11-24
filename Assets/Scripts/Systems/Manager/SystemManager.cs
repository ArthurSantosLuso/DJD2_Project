using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    public enum PauseType { StopEverything, Inspection, FocusObject}

    private static SystemManager _instance;

    public static SystemManager Instance
    {
        get
        {
            if (_instance == null)
                FindFirstObjectByType<SystemManager>().Init();

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
            Init();
        else if (_instance != this)
            Destroy(gameObject);
    }

    private void Init()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] private FirstPersonMovement playerMovement;
    [SerializeField] private PlayerInteractor playerInteractor;

    private void Start()
    {
        UnpauseGame();
    }

    public void PauseGame(PauseType pauseType)
    {
        if(pauseType == PauseType.StopEverything)
            Time.timeScale = 0f;
        DisablePlayer(pauseType);
        CursorEnable();
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1.0f;
        EnablePlayer();
        CursorDisable();
    }

    private void DisablePlayer(PauseType pauseType)
    {
        if (pauseType != PauseType.FocusObject)
            playerInteractor.enabled = false;

        playerMovement.enabled = false;
    }

    private void EnablePlayer()
    {
        playerInteractor.enabled = true;
        playerMovement.enabled = true;
    }

    private void CursorDisable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void CursorEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

}
