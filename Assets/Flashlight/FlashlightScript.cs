using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class FlashlightScript : MonoBehaviour
{
    [SerializeField] private GameObject FlashlightLight; // Assign your flashlight GameObject in the Inspector
    private bool FlashlightActive = false;


    private UnityEngine.XR.InputDevice _device_leftController;
    private UnityEngine.XR.InputDevice _device_rightController;

    private Vector3 _inputVelocity_leftController;
    private Vector3 _inputVelocity_rightController;



    // Start is called before the first frame update
    void Start()
    {


        _device_leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        _device_rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        // Ensure the flashlight starts off
        FlashlightLight.SetActive(false);
    }
    void Update()
    {
        getInput();

        Debug.Log(_inputVelocity_leftController.ToString());
        Debug.Log (_inputVelocity_rightController.ToString());

    }

    public void getInput()
    {
        _device_leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out _inputVelocity_leftController);
        _device_rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out _inputVelocity_rightController);
    }

    public void FlashLightButton(InputAction.CallbackContext context)
    {
        // Check if the button is pressed 
        if (context.performed)
        {
            FlashlightActive = !FlashlightActive; // Toggle the flashlight state
            FlashlightLight.SetActive(FlashlightActive); // Enable or disable the light
            Debug.Log($"Flashlight toggled: {FlashlightActive}");
        }
    }
}
