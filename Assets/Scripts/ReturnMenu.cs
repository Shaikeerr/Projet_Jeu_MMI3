using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ReturnMenu : MonoBehaviour
{

    public PlayerControls inputActions;

    private void Awake()
    {
       inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        inputActions.Player.Menu.Enable();
        inputActions.Player.Menu.performed += OnMenu;
    }
    private void OnDisable()
    {
        inputActions.Player.Menu.Disable();
        inputActions.Player.Menu.performed -= OnMenu;
    }

    private void OnMenu(InputAction.CallbackContext context)
    {
        GetBackToMenu();
    }

    public void GetBackToMenu()
    {
        SceneManager.LoadScene(0); 
        Time.timeScale = 0;

        if (CharacterManager.CharacterInstance != null)
    {
        Destroy(CharacterManager.CharacterInstance.gameObject);
    }
    
    }
}

    