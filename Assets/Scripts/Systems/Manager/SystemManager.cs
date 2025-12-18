using UnityEngine;

public class SystemManager : MonoBehaviour
{
    public enum PauseType { StopEverything, Inspection, FocusObject, PhonePause }

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
        //DontDestroyOnLoad(gameObject);
    }

    [SerializeField] private FirstPersonMovement playerMovement;
    [SerializeField] private PlayerInteractor playerInteractor;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private UIManager uiManager;

    private void Start()
    {
        //Debug.Log("Passei no Start do SystemManager :D");
        UnpauseGame();
        //Debug.Log($"Player Interactor state {playerInteractor.enabled}");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (playerMovement.enabled) PauseGame(PauseType.StopEverything);
            else UnpauseGame();

            uiManager.TogglePauseScreen();
        }
    }

    public void PauseGame(PauseType pauseType)
    {
        if (pauseType == PauseType.StopEverything)
            Time.timeScale = 0f;
        DisablePlayer(pauseType);
        CursorEnable();
    }

    public void UnpauseGame(PauseType pauseType = PauseType.Inspection)
    {
        if (pauseType == PauseType.PhonePause)
        {
            playerInteractor.enabled = true;
        }
        else
        {
            Time.timeScale = 1.0f;
            EnablePlayer();
            CursorDisable();
        }
    }


    //public void DisableInteraction(int seconds)
    //{
    //    StartCoroutine(DisableInteracionWait(seconds));
    //}

    //private IEnumerator DisableInteracionWait(int seconds)
    //{

    //}

    private void DisablePlayer(PauseType pauseType)
    {
        if (pauseType != PauseType.FocusObject)
            playerInteractor.enabled = false;

        playerMovement.enabled = false;
        playerInventory.enabled = false;
    }

    private void EnablePlayer()
    {
        playerInteractor.enabled = true;
        playerMovement.enabled = true;
        playerInventory.enabled = true;
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
