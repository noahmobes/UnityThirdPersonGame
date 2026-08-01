using UnityEngine;
using UnityEngine.InputSystem;

public class GameGuiController : MonoBehaviour
{
    [Header("Scene Manager")]
    public SceneLoader sceneManager;
    [Header("Pause Menu")]
    public GameObject settingsMenu;
    [Header("Player Character")]
    public ThirdPersonMovement player;
    [Header("Hud")]
    public GameObject hud;
    private InputAction openSettingsAction;

    private void Start()
    {
        settingsMenu.SetActive(false);
        hud.SetActive(true);
        Reset();
    }

    private void Awake()
    {
        openSettingsAction = InputSystem.actions.FindAction("Pause");
 
        if (openSettingsAction != null)
        {
            openSettingsAction.performed += OnOpenSettings;
        }
    }

    private void OnEnable()
    {
        InputSystem.actions.Enable();
    }

    private void OnOpenSettings(InputAction.CallbackContext ctx)
    {
        OpenSettings();
    }

    private void OpenSettings()
    {
        PauseGame();
        settingsMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        ResumeGame();
    }

    public void LoadStartScene()
    {
        Reset();
        sceneManager.LoadStartScene();
    }

    public void QuitGame()
    {
        sceneManager.QuitGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        hud.SetActive(false);
        player.GetComponent<ThirdPersonMovement>().enabled = false;
    }

    private void ResumeGame()
    {
        Reset();
    }

    private void Reset()
    {
        player.GetComponent<ThirdPersonMovement>().enabled = true;
        Time.timeScale = 1;
        settingsMenu.SetActive(false);
        hud.SetActive(true);
    }
}