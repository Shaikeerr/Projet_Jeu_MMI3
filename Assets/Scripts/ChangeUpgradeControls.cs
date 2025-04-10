using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ChangeUpgradeControls : MonoBehaviour
{

    public Image LeftUpgradeControlsImage;
    public Image RightUpgradeControlsImage; 

    [Header("Gamepad Sprites")]
    public Sprite XBOXLB; // Left Bumper || LeftUpgradeButton
    public Sprite XBOXRB; // Right Bumper || RightUpgradeButton
    public Sprite PSL1; // Left Bumper || LeftUpgradeButton
    public Sprite PSR1; // Right Bumper || RightUpgradeButton
    public Sprite NoneSprite; // Nothing is no gamepad is detected



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
        var activeDevice = InputSystem.GetDevice<InputDevice>();

        if (activeDevice != null)
        {
            Debug.Log("Active device detected: " + activeDevice.displayName);
            if (activeDevice is Gamepad gamepad)
            {
                if (gamepad.displayName.Contains("Xbox"))
                {
                    LeftUpgradeControlsImage.sprite = XBOXLB;
                    RightUpgradeControlsImage.sprite = XBOXRB;
                }
                else if (gamepad.displayName.Contains("DualShock") || gamepad.displayName.Contains("DualSense")) // Check for PlayStation controllers (PS3/PS4/PS5)
                {
                    LeftUpgradeControlsImage.sprite = PSL1; 
                    RightUpgradeControlsImage.sprite = PSR1;
                }
            }
            else
            {
                Debug.Log("No gamepad detected.");
                LeftUpgradeControlsImage.sprite = NoneSprite;
                RightUpgradeControlsImage.sprite = NoneSprite; 
            }
        }
    }
}