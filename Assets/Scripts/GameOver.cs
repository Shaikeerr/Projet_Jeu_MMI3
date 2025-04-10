using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameOver : MonoBehaviour
{
    public Text waveText;
    private SummonEnnemies summonEnnemies;

    public PlayerControls inputActions;
    public Button retryButton;

    void Awake()
    {
        inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        inputActions.Menu.SelectReset.Enable();
        inputActions.Menu.SelectReset.performed += OnSelectRetry;
    }

    private void OnDisable()
    {
        inputActions.Menu.SelectReset.Disable();
        inputActions.Menu.SelectReset.performed -= OnSelectRetry;
    }

    private void OnSelectRetry(InputAction.CallbackContext context)
    {
        if (retryButton != null)
        {
            retryButton.onClick.Invoke();
        }
    }


    void Start()
    {
        summonEnnemies = FindObjectOfType<SummonEnnemies>();

        if (summonEnnemies != null)
        {
            if (summonEnnemies.currentWave == 1)
            {
                waveText.text = "Vous avez survécu " + summonEnnemies.currentWave.ToString() + " vague !"; 
            }
            
            waveText.text = "Vous avez survécu " + summonEnnemies.currentWave.ToString() + " vagues !";
        }
        else
        {
            Debug.LogError("SummonEnnemies component not found in the scene.");
        }
    }

    void Update()
    {
        
    }
}
