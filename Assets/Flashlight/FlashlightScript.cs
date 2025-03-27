using Helpers;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class FlashlightScript : MonoBehaviour
{
    public Light LightSource; //flashlight
    public Collider LightInteractor;
    public bool FlashlightActive = false;


    
    public InputData _inputData;

    private float _pastTime = 0.25f;   //Measuring the velocity of the controller every 0.25 seconds
    private float _battery = 10f;     //battery life


    private readonly float smoothingFactor = 0.2f; //helping with noise from the controllers


    private Vector3 _cachedLeftVelocity = Vector3.zero;  //tracking the past velocity for measuring % change to current velocity
    private Vector3 _cachedRightVelocity = Vector3.zero;


   




    // Start is called before the first frame update
    void Start()
    {

        _inputData = GetComponent<InputData>();
       
        if(_inputData == null)
        {
            UnityEngine.Debug.Log("Input data script is missing");
        }



        // Ensure the flashlight starts off
        LightSource.enabled = false;


        //Setting the light interactor to be off
        LightInteractor.enabled = false;



    }
    void Update()
    {
        if (FlashlightActive == true)  // Flashlight battery drains when held
        {
            _battery -= Time.deltaTime;
            
        }

        if (_battery < 0) // Battery dies
        {
            LightInteractor.enabled = false;
            LightSource.enabled = false;
            FlashlightActive = false;
        }

        _pastTime -= Time.deltaTime;





        RightVelocityCheck();


        LeftVelocityCheck();
        





        // Store past velocity every 0.5s
        if (_pastTime <= 0)
        {
            if (_inputData._leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out Vector3 currentLeftVelocity))
            {
                _cachedLeftVelocity = currentLeftVelocity;

               

            }

            if (_inputData._rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out Vector3 currentRightVelocity))
            {
                _cachedRightVelocity = currentRightVelocity;


                

            }


            _pastTime = 0.25f; // Reset the timer


        }




        

    }


    //public void OnGrab(SelectEnterEventArgs context)
    //{
    //    inHand = true;
    //    UnityEngine.Debug.Log("Flashlight grabbed");
    //}


    //public void OnRelease(SelectExitEventArgs context)
    //{

    //    inHand = false;
    //    UnityEngine.Debug.Log("Flashlight Dropped");
    //}




    //Grabbing the flashlight
    //public void OnFlashlightGrab(SelectEnterEventArgs _)  
    //{
    //    inHand = true;
    //    UnityEngine.Debug.Log("Flashlight grabbed");
    //}

    ////Dropping flashlight
    //public void OnFlashlightRelease(SelectExitEventArgs _)
    //{
    //    inHand = false;
    //}





    public UnityEvent<InputAction> TogglePower = new();
    public void OnTogglePower(InputAction inputAction)
    {
        int triggerInput = Math.RoundNearestNonZeroInt(inputAction.ReadValue<float>());
        UnityEngine.Debug.Log(triggerInput);
        if (triggerInput == 1)
        {
            FlashlightActive = !FlashlightActive;
            LightSource.enabled = !LightSource.enabled;
            LightInteractor.enabled = !LightInteractor.enabled;
            UnityEngine.Debug.Log($"Flashlight toggled: {FlashlightActive}");
            
        }
    }



    //Checking if the primary button on either controller is in hand
    //public void FlashLightButton(InputAction.CallbackContext context)
    //{
    //    // Check if the button is pressed 
    //    if (context.performed && inHand)
    //    {

    //            FlashlightActive = !FlashlightActive; // Toggle the flashlight state
    //            FlashLightLight.SetActive(FlashlightActive); // Enable or disable the light
    //            LightInteractor.enabled = true;

    //            Debug.Log($"Flashlight toggled: {FlashlightActive}");



    //    }
    //}



    public void RightVelocityCheck()
    {
        //checking past velocity for the right controller
        if (_inputData._rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out Vector3 rightVelocity))
        {

            float pastRightMagnitude = _cachedRightVelocity.magnitude;
            float currentRightMagnitude = rightVelocity.magnitude;
            _cachedRightVelocity = Vector3.Lerp(_cachedRightVelocity, rightVelocity, smoothingFactor);

            if (pastRightMagnitude > 0.2f) //checking to make sure we arent dividing by an insignificant amount to filter out noise
            {
                float RightPercentageIncrease = Mathf.Abs((currentRightMagnitude - pastRightMagnitude) / pastRightMagnitude) * 100f;   //Math for checking the percentage increase between past magnitude and current magnitude
                //Debug.Log(RightPercentageIncrease);


                if (RightPercentageIncrease >= 225f && currentRightMagnitude > 0.7f) //checking to see if the current right magnitude increased by 50%
                {
                    _battery = 10f;
                    UnityEngine.Debug.Log("Right charged the battery");
                }


            }
        }
    }


    public void LeftVelocityCheck()
    {


        //checking past velocity of 
        if (_inputData._leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out Vector3 leftVelocity))
        {

            float pastLeftMagnitude = _cachedLeftVelocity.magnitude;
            float currentLeftMagnitude = leftVelocity.magnitude;
            _cachedLeftVelocity = Vector3.Lerp(_cachedLeftVelocity, leftVelocity, smoothingFactor);

            if (pastLeftMagnitude > 0.2f) //checking to make sure we arent dividing by an insignificant amount and filtering out noise
            {
                float LeftPercentageIncrease = Mathf.Abs((currentLeftMagnitude - pastLeftMagnitude) / pastLeftMagnitude) * 100f;   //Math for checking the percentage increase between past magnitude and current magnitude
                //Debug.Log(LeftPercentageIncrease);


                if (LeftPercentageIncrease >= 225f && currentLeftMagnitude > 0.7f) //checking to see if the current right magnitude increased by 50%
                {
                    _battery = 10f;
                    UnityEngine.Debug.Log("Left charged the battery");
                }


            }
        }
    }









}
