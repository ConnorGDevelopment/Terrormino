using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class FlashlightScript : MonoBehaviour
{
    [SerializeField] private GameObject FlashLightLight; // Assign your flashlight GameObject in the Inspector
    private bool FlashlightActive = false;



    private InputData _inputData;

    private float timer = 10f;



    // Start is called before the first frame update
    void Start()
    {

        _inputData = GetComponent<InputData>();
       

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


        
        if(_inputData._leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out Vector3 leftVelocity))
        {
            Debug.Log(leftVelocity.magnitude.ToString());
            if (leftVelocity.magnitude > 1)
            {
                timer = 10f;
            }
        }

        if (_inputData._leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out Vector3 rightVelocity))

        {
            Debug.Log(rightVelocity.magnitude.ToString());

            if(rightVelocity.magnitude > 1)
            {
                timer = 10f;
            }
        }




    }

   

    public void FlashLightButton(InputAction.CallbackContext context)
    {
        // Check if the button is pressed 
        if (context.performed)
        {
            
            
            FlashlightActive = !FlashlightActive; // Toggle the flashlight state
            FlashLightLight.SetActive(FlashlightActive); // Enable or disable the light
            Debug.Log($"Flashlight toggled: {FlashlightActive}");
            
        }
    }
}
