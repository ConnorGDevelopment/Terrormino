using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class FlashlightScript : MonoBehaviour
{
    [SerializeField] private GameObject FlashLightLight; //flashlight
    [SerializeField] private GameObject LightInteractor;
    public bool FlashlightActive = false;


    private bool inHand = false;
    [SerializeField] private InputData _inputData;

    private float _pastTime = 0.25f;   //Measuring the velocity of the controller every 0.25 seconds
    private float _battery = 10f;     //battery life


    private float smoothingFactor = 0.2f; //helping with noise from the controllers


    private Vector3 PastLeftVelocity = Vector3.zero;  //tracking the past velocity for measuring % change to current velocity
    private Vector3 PastRightVelocity = Vector3.zero;


   




    // Start is called before the first frame update
    void Start()
    {

        _inputData = GetComponent<InputData>();
       
        if(_inputData == null)
        {
            Debug.Log("Input data script is missing");
        }



        // Ensure the flashlight starts off
        FlashLightLight.SetActive(false);


        //Setting the light interactor to be off
        LightInteractor.SetActive(false);



    }
    void Update()
    {
        if (inHand && FlashlightActive == true)  // Flashlight battery drains when held
        {
            _battery -= Time.deltaTime;
            
        }

        if (_battery < 0) // Battery dies
        {
            LightInteractor.SetActive(false);
            FlashLightLight.SetActive(false);
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
                PastLeftVelocity = currentLeftVelocity;

               

            }

            if (_inputData._rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out Vector3 currentRightVelocity))
            {
                PastRightVelocity = currentRightVelocity;


                

            }


            _pastTime = 0.25f; // Reset the timer


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
                LightInteractor.SetActive(true);

                Debug.Log($"Flashlight toggled: {FlashlightActive}");
            
            
            
        }
    }



    public void RightVelocityCheck()
    {
        //checking past velocity for the right controller
        if (_inputData._rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out Vector3 rightVelocity))
        {

            float pastRightMagnitude = PastRightVelocity.magnitude;
            float currentRightMagnitude = rightVelocity.magnitude;
            PastRightVelocity = Vector3.Lerp(PastRightVelocity, rightVelocity, smoothingFactor);

            if (pastRightMagnitude > 0.2f) //checking to make sure we arent dividing by an insignificant amount to filter out noise
            {
                float RightPercentageIncrease = Mathf.Abs((currentRightMagnitude - pastRightMagnitude) / pastRightMagnitude) * 100f;   //Math for checking the percentage increase between past magnitude and current magnitude
                //Debug.Log(RightPercentageIncrease);


                if (RightPercentageIncrease >= 225f && currentRightMagnitude > 0.7f && inHand) //checking to see if the current right magnitude increased by 50%
                {
                    _battery = 10f;
                    Debug.Log("Right charged the battery");
                }


            }
        }
    }


    public void LeftVelocityCheck()
    {


        //checking past velocity of 
        if (_inputData._leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out Vector3 leftVelocity))
        {

            float pastLeftMagnitude = PastLeftVelocity.magnitude;
            float currentLeftMagnitude = leftVelocity.magnitude;
            PastLeftVelocity = Vector3.Lerp(PastLeftVelocity, leftVelocity, smoothingFactor);

            if (pastLeftMagnitude > 0.2f) //checking to make sure we arent dividing by an insignificant amount and filtering out noise
            {
                float LeftPercentageIncrease = Mathf.Abs((currentLeftMagnitude - pastLeftMagnitude) / pastLeftMagnitude) * 100f;   //Math for checking the percentage increase between past magnitude and current magnitude
                //Debug.Log(LeftPercentageIncrease);


                if (LeftPercentageIncrease >= 225f && currentLeftMagnitude > 0.7f && inHand) //checking to see if the current right magnitude increased by 50%
                {
                    _battery = 10f;
                    Debug.Log("Left charged the battery");
                }


            }
        }
    }









}
