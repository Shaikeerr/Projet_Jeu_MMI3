using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour
{

    private CharacterStats characterStats;
    public float moveSpeed;
    private PlayerControls inputActions;
    private Vector2 movementInput;

    private void Awake()
    {
        inputActions = new PlayerControls();


        characterStats = GetComponentInParent<CharacterStats>();
        if (characterStats != null)
        {
            moveSpeed = characterStats.Speed;
        }
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Movement.performed += OnMove;
        inputActions.Player.Movement.canceled += OnMove;
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.Movement.performed -= OnMove;
        inputActions.Player.Movement.canceled -= OnMove;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {

        if (characterStats != null)
        {
            moveSpeed = characterStats.Speed;
        }

        Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y); 
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }
}