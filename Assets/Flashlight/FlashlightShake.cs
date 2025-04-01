using Helpers;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class FlashlightShake : MonoBehaviour
{
    public Light LightSource; //flashlight
    public Collider LightInteractor;  //Collider for checking if the demon is inside
    public bool FlashlightActive = false;     //flag for knowing if the flashlight is currently active


    
    public InputData _inputData;     //Reference script to grab device characteristics, used for finding the device velocity

    private float _pastTime = 0.25f;   //Measuring the velocity of the controller every 0.25 seconds
    private float _battery = 5f;     //battery life


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
            _battery -= Time.deltaTime;   //Battery continually loses charge
            
        }

        if (_battery < 0) // Battery dies
        {
            LightInteractor.enabled = false;   //Resetting everything back to being inactive/off
            LightSource.enabled = false;
            FlashlightActive = false;
        }

        _pastTime -= Time.deltaTime;      //flag for checking the timing for whether or not the device velocity should be checked





        RightVelocityCheck();          //Checking for right controller every frame and seeing if there is a significant increase between the past velocity and current velocity


        LeftVelocityCheck();           //Checking for left controller
        





        // Store past velocity every 0.5s
        if (_pastTime <= 0)    
        {
            if (_inputData._leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out Vector3 currentLeftVelocity))        //Using the input data script to cache the magnitude of the device velocity
            {
                _cachedLeftVelocity = currentLeftVelocity;

               

            }

            if (_inputData._rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out Vector3 currentRightVelocity))       //Same thing but for right controller
            {
                _cachedRightVelocity = currentRightVelocity;


                

            }


            _pastTime = 0.25f; // Reset the timer


        }




        

    }






    public UnityEvent<InputAction> TogglePower = new();                
    public void OnTogglePower(InputAction inputAction)                                         //By using the input router we can check what device was being used to press the button that triggers the unity event
    {
        int triggerInput = Math.RoundNearestNonZeroInt(inputAction.ReadValue<float>());          //Putting the value of the button into either a 0 or 1
        UnityEngine.Debug.Log(triggerInput);                   
        if (triggerInput == 1)                          //If the value is 1 that means the button has been pressed and the flashlight can be active
        {
            FlashlightActive = !FlashlightActive;
            LightSource.enabled = !LightSource.enabled;
            LightInteractor.enabled = !LightInteractor.enabled;
            UnityEngine.Debug.Log($"Flashlight toggled: {FlashlightActive}");
            
        }
    }



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


                if (RightPercentageIncrease >= 225f && currentRightMagnitude > 0.7f) //checking to see if the current right magnitude increased by 225% (i.e. shaking)
                {
                    _battery = 10f;
                    UnityEngine.Debug.Log("Right charged the battery");
                }


            }
        }
    }


    public void LeftVelocityCheck()
    {


        //checking past velocity of the left controller
        if (_inputData._leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out Vector3 leftVelocity))
        {

            float pastLeftMagnitude = _cachedLeftVelocity.magnitude;
            float currentLeftMagnitude = leftVelocity.magnitude;
            _cachedLeftVelocity = Vector3.Lerp(_cachedLeftVelocity, leftVelocity, smoothingFactor);

            if (pastLeftMagnitude > 0.2f) //checking to make sure we arent dividing by an insignificant amount and filtering out noise
            {
                float LeftPercentageIncrease = Mathf.Abs((currentLeftMagnitude - pastLeftMagnitude) / pastLeftMagnitude) * 100f;   //Math for checking the percentage increase between past magnitude and current magnitude
                //Debug.Log(LeftPercentageIncrease);


                if (LeftPercentageIncrease >= 225f && currentLeftMagnitude > 0.7f) //checking to see if the current right magnitude increased by 225% (i.e. shaking)
                {
                    _battery = 10f;
                    UnityEngine.Debug.Log("Left charged the battery");
                }


            }
        }
    }









}
