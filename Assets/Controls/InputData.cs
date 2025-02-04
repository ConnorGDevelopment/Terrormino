using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

public class InputData : MonoBehaviour
{

    //THIS SCRIPTS WHOLE PURPOSE IS TO GRAB THE VELOCITY OF THE CONTROLLERS, THIS WILL NOT MESS WITH ANY OF THE INPUT MAPS OR ANYTHING, THIS IS JUST FOR FINDING THE DATA THAT UNITY GIVES BECAUSE UNITY WILL NOT DIRECTLY GIVE DEVICE VELOCITY

    public InputDevice _rightController;
    public InputDevice _leftController;
    public InputDevice _HMD;





    // Update is called once per frame
    void Update()
    {
        if (!_rightController.isValid || !_leftController.isValid || !_HMD.isValid)
        {
            InitializeInputDevices();
        }
    }


    private void InitializeInputDevices()
    {
        if (!_rightController.isValid)
        {
            InitializeInputDevice(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, ref _rightController);
        }

        if (!_leftController.isValid)
        {
            InitializeInputDevice(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, ref _leftController);
        }

        if (!_HMD.isValid)
        {
            InitializeInputDevice(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, ref _HMD);
        }


    }


    private void InitializeInputDevice(InputDeviceCharacteristics inputCharacteristics, ref InputDevice inputDevice)
    {
        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(inputCharacteristics, devices);

        if (devices.Count > 0)
        {
            inputDevice = devices[0];
        }


    }
}
