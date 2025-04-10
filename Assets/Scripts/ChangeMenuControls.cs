using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ChangeMenuControls : MonoBehaviour
{

    public Image controlsImage; 

    [Header("Gamepad Sprites")]
    public Sprite XBOXSprites;
    public Sprite PSSprites;
    public Sprite NoneSprite;



    private void Start()
    {
        UpdateControlsDisplay();
    }

    private void OnEnable()
    {
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (change == InputDeviceChange.Added || change == InputDeviceChange.Removed)
        {
            UpdateControlsDisplay();
        }
    }

    private void UpdateControlsDisplay()
    {
        var activeDevice = InputSystem.GetDevice<InputDevice>(); // Get the currently active device

        if (activeDevice != null)
        {
            Debug.Log("Active device detected: " + activeDevice.displayName); // Log the name of the active device
            if (activeDevice is Gamepad gamepad)
            {
                if (gamepad.displayName.Contains("Xbox"))
                {
                    controlsImage.sprite = XBOXSprites; 
                }
                else if (gamepad.displayName.Contains("DualShock") || gamepad.displayName.Contains("DualSense"))
                {
                    controlsImage.sprite = PSSprites; // Set PlayStation sprite if DualShock (PS3/PS4) or DualSense (PS5) is detected
                }
            }
            else
            {
                Debug.Log("No gamepad detected.");
                controlsImage.sprite = NoneSprite; // Set None sprite if no gamepad is detected (Keyboard/Mouse)
            }
        }
    }
}