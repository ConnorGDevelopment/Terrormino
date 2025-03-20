using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace Flashlight
{
    public class Shake : MonoBehaviour
    {
        public Light _lightSource; // GameObject with Light Source
        public Collider _lightInteractor;
        public bool FlashlightActive = false;

        private bool _isGrabbed = false;
        public InputData _inputData;

        private float _lastTimestamp = 0.25f;   // Measuring the velocity of the controller every 0.25 seconds
        private float _batteryLife = 10f;

        private readonly float _smoothingFactor = 0.2f; // helping with noise from the controllers

        private Vector3 _cachedLeftVelocity = Vector3.zero;  // tracking the past velocity for measuring % change to current velocity
        private Vector3 _cachedRightVelocity = Vector3.zero;

        void Start()
        {
            _inputData = Helpers.Debug.TryFindComponent<InputData>(gameObject);
            _lightSource.enabled = false;
        }
        void Update()
        {
            if (_isGrabbed && FlashlightActive)  // Flashlight battery drains when held
            {
                _batteryLife -= Time.deltaTime;
            }

            if (_batteryLife < 0) // Battery dies
            {
                _lightInteractor.enabled = false;
                _lightSource.enabled = false;
                FlashlightActive = false;
            }
            _lastTimestamp -= Time.deltaTime;

            RightVelocityCheck();
            LeftVelocityCheck();

            // Store past velocity every 0.5s
            if (_lastTimestamp <= 0)
            {
                if (_inputData._leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out Vector3 currentLeftVelocity))
                {
                    _cachedLeftVelocity = currentLeftVelocity;
                }

                if (_inputData._rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out Vector3 currentRightVelocity))
                {
                    _cachedRightVelocity = currentRightVelocity;
                }
                _lastTimestamp = 0.25f; // Reset the timer
            }
        }

        public void OnGrab(SelectEnterEventArgs _)
        {
            _isGrabbed = true;
        }

        public void OnRelease(SelectExitEventArgs _)
        {
            _isGrabbed = false;
        }


        public UnityEvent<InputAction> TogglePower = new();
        public void OnTogglePower(InputAction inputAction)
        {
            FlashlightActive = !FlashlightActive;
            _lightSource.enabled = !_lightSource.enabled;
            _lightInteractor.enabled = !_lightInteractor.enabled;
            Debug.Log($"Flashlight toggled: {FlashlightActive}");
        }

        public void RightVelocityCheck()
        {
            //checking past velocity for the right controller
            if (_inputData._rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out Vector3 rightVelocity))
            {
                float pastRightMagnitude = _cachedRightVelocity.magnitude;
                float currentRightMagnitude = rightVelocity.magnitude;
                _cachedRightVelocity = Vector3.Lerp(_cachedRightVelocity, rightVelocity, _smoothingFactor);

                if (pastRightMagnitude > 0.2f) //checking to make sure we arent dividing by an insignificant amount to filter out noise
                {
                    float RightPercentageIncrease = Mathf.Abs((currentRightMagnitude - pastRightMagnitude) / pastRightMagnitude) * 100f;   //Math for checking the percentage increase between past magnitude and current magnitude

                    if (RightPercentageIncrease >= 225f && currentRightMagnitude > 0.7f && _isGrabbed) //checking to see if the current right magnitude increased by 50%
                    {
                        _batteryLife = 10f;
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
                float pastLeftMagnitude = _cachedLeftVelocity.magnitude;
                float currentLeftMagnitude = leftVelocity.magnitude;
                _cachedLeftVelocity = Vector3.Lerp(_cachedLeftVelocity, leftVelocity, _smoothingFactor);

                if (pastLeftMagnitude > 0.2f) //checking to make sure we arent dividing by an insignificant amount and filtering out noise
                {
                    float LeftPercentageIncrease = Mathf.Abs((currentLeftMagnitude - pastLeftMagnitude) / pastLeftMagnitude) * 100f;   //Math for checking the percentage increase between past magnitude and current magnitude

                    if (LeftPercentageIncrease >= 225f && currentLeftMagnitude > 0.7f && _isGrabbed) //checking to see if the current right magnitude increased by 50%
                    {
                        _batteryLife = 10f;
                        Debug.Log("Left charged the battery");
                    }
                }
            }
        }
    }
}
