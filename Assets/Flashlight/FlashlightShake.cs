using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class FlashlightShake : FlashlightScript
{
    [SerializeField] private GameObject FlashLightLight; // Assign your flashlight GameObject in the Inspector
    private bool FlashlightActive = false;



    private UnityEngine.XR.InputDevice _device_leftController;
    private UnityEngine.XR.InputDevice _device_rightController;

    private Vector3 _inputVelocity_leftController;
    private Vector3 _inputVelocity_rightController;

    private float timer = 10f;



    // Start is called before the first frame update
    void Start()
    {


        _device_leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        _device_rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        // Ensure the flashlight starts off
        FlashLightLight.SetActive(false);
    }
    void Update()
    {

        timer = timer - Time.deltaTime;

        if (timer < 0)
        {
            FlashLightLight.SetActive(false);
        }


        getInput();

        Debug.Log(_inputVelocity_leftController.ToString());
        Debug.Log(_inputVelocity_rightController.ToString());


        if (_inputVelocity_leftController.x > 1 | _inputVelocity_leftController.y > 1 | _inputVelocity_leftController.z > 1 | _inputVelocity_rightController.x < 1 | _inputVelocity_rightController.y < 1 | _inputVelocity_rightController.z < 1)
        {
            timer = 10;
        }



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
            if (timer! <= 0)
            {
                FlashlightActive = !FlashlightActive; // Toggle the flashlight state
                FlashLightLight.SetActive(FlashlightActive); // Enable or disable the light
                Debug.Log($"Flashlight toggled: {FlashlightActive}");
            }
        }
    }
}