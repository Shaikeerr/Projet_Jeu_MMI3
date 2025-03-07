using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ReturnMenu : MonoBehaviour
{

    public InputAction ReturnToMenu;

    void Awake()
    {
        var inputActionAsset = new InputActionAsset();
    }

    private void OnEnable()
    {
        ReturnToMenu.Enable();
    }

    private void OnDisable()
    {
        ReturnToMenu.Disable();
    }

    void Start()
    {
        ReturnToMenu.performed += ctx => GoBackToMenu();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoBackToMenu();
        }
    }

    void GoBackToMenu()
    {
        Debug.Log("Retour au menu !");
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }
}
