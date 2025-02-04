using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class FlashlightScript : MonoBehaviour
{
    [SerializeField] private GameObject FlashLightLight; // Assign your flashlight GameObject in the Inspector
    private bool FlashlightActive = false;


    private bool inHand = false;
    private InputData _inputData;

    private float _pastTime = 0.5f;
    private float _battery = 10f;

    private Vector3 PastLeftVelocity = Vector3.zero;
    private Vector3 PastRightVelocity = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {

        _inputData = GetComponent<InputData>();
       

        // Ensure the flashlight starts off
        FlashLightLight.SetActive(false);
    }
    void Update()
    {
        if (inHand)  // Flashlight battery drains when held
        {
            _battery -= Time.deltaTime;
        }

        if (_battery < 0) // Battery dies
        {
            FlashLightLight.SetActive(false);
        }

        _pastTime -= Time.deltaTime;

        // Store past velocity every 0.5s
        if (_pastTime <= 0)
        {
            if (_inputData._leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out Vector3 currentLeftVelocity))
            {
                PastLeftVelocity = currentLeftVelocity;
            }

            if (_inputData._rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out Vector3 currentRightVelocity))
            {
                PastRightVelocity = currentRightVelocity;
            }

            _pastTime = 0.5f; // Reset the timer
        }

        // Measure change in left controller velocity
        if (_inputData._leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out Vector3 leftVelocity))
        {
            float LeftMagnitudeChange = Mathf.Abs(leftVelocity.magnitude - PastLeftVelocity.magnitude);
            Debug.Log($"Left Magnitude Change: {LeftMagnitudeChange}");

            if (LeftMagnitudeChange > 0.5f) // Sensitivity threshold
            {
                _battery = 10f;
            }
        }

        // Measure change in right controller velocity
        if (_inputData._rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out Vector3 rightVelocity))
        {
            float RightMagnitudeChange = Mathf.Abs(rightVelocity.magnitude - PastRightVelocity.magnitude);
            //Debug.Log($"Right Magnitude Change: {RightMagnitudeChange}");

            if (RightMagnitudeChange > 0.5f)
            {
                _battery = 10f;
            }
        }
    }





//Grabbing the flashlight
public void OnFlashlightGrab(SelectEnterEventArgs _)  
    {
        inHand = true;
    }

    //Dropping flashlight
    public void OnFlashlightRelease(SelectExitEventArgs _)
    {
        inHand = false;
    }


   
    //Checking if the primary button on either controller is in hand
    public void FlashLightButton(InputAction.CallbackContext context)
    {
        // Check if the button is pressed 
        if (context.performed && inHand)
        {
            
            
            FlashlightActive = !FlashlightActive; // Toggle the flashlight state
            FlashLightLight.SetActive(FlashlightActive); // Enable or disable the light
            Debug.Log($"Flashlight toggled: {FlashlightActive}");
            
        }
    }
}
