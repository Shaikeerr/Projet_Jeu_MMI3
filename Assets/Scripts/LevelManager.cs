using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class LevelMenuManager : MonoBehaviour
{

    private PlayerControls inputActions;

    public Button gameLevelButton;
    public Button debugLevelButton;

    private void Awake()
    {
        inputActions = new PlayerControls();
    }   

    private void OnEnable()
    {
        inputActions.Menu.SelectGameLevel.Enable();
        inputActions.Menu.SelectDebugLevel.Enable();

        inputActions.Menu.SelectGameLevel.performed += OnSelectGameLevel;
        inputActions.Menu.SelectDebugLevel.performed += OnSelectDebugLevel;
    }

    private void OnDisable()
    {
        inputActions.Menu.SelectGameLevel.Disable();
        inputActions.Menu.SelectDebugLevel.Disable();
    }

    private void OnSelectGameLevel(InputAction.CallbackContext context)
    {
        if (gameLevelButton != null)
        {
            gameLevelButton.onClick.Invoke();
        }
    }

    private void OnSelectDebugLevel(InputAction.CallbackContext context)
    {
        if (debugLevelButton != null)
        {
            debugLevelButton.onClick.Invoke();
        }
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
        Time.timeScale = 1;
    }
}
