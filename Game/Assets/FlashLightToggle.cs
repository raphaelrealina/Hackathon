using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class FlashlightToggle : MonoBehaviour
{
    public Light flashlightLight;
    private bool isOn = false;

    public XRNode inputSource = XRNode.RightHand; // or LeftHand
    private InputDevice device;
    private bool previousButtonState = false; // track previous frame

    void Start()
    {
        device = InputDevices.GetDeviceAtXRNode(inputSource);
    }

    void Update()
    {
        if (!device.isValid)
        {
            // Try to get device again if it's not valid
            device = InputDevices.GetDeviceAtXRNode(inputSource);
            return;
        }

        if (device.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonPressed))
        {
            // Only toggle when button is pressed this frame (not held down)
            if (primaryButtonPressed && !previousButtonState)
            {
                ToggleFlashlight();
            }

            previousButtonState = primaryButtonPressed;
        }
    }

    public void ToggleFlashlight()
    {
        isOn = !isOn;
        flashlightLight.enabled = isOn;
        Debug.Log("Flashlight toggled: " + (isOn ? "ON" : "OFF"));
    }
}
