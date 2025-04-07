using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace Flashlight
{
    public class Shake : MonoBehaviour
    {
        public Light LightSource;
        public Collider LightCollider;
        public XRGrabInteractable GrabInteractable;
        public bool Active = false;

        public float BatteryMax = 5f;
        private float _battery = 5f;
        public float Battery
        {
            get { return _battery; }
            set
            {
                _battery = Mathf.Clamp(value, 0, 5f);
            }
        }

        public UnityEvent<InputAction> TogglePower = new();
        public void OnTogglePower(InputAction _)
        {
            Active = !Active;
            LightSource.enabled = Active;
        }
        public void OnTogglePower(bool value)
        {
            Active = value;
            LightSource.enabled = Active;
        }

        private Vector3 _cachedPosition = Vector3.zero;
        private Vector3 _cachedVelocity = Vector3.zero;
        public void Charge(Vector3 position, float deltaTime)
        {
            if (GrabInteractable.isSelected && !Active)
            {
                var velocity = (position - _cachedPosition) / deltaTime;

                // Signed Angle basically draws angle using a third reference point
                // The angle between the previous velocity and the current velocity is the change in direction
                // We know the device is being shaken if the velocity is changing direction from the reference point of the player
                if (Vector3.SignedAngle(velocity, _cachedVelocity, gameObject.transform.position) > 0)
                {
                    Battery += deltaTime * 2;
                }

                _cachedVelocity = velocity;
                _cachedPosition = position;
            }
        }



        public void Start()
        {
            LightSource = Helpers.Debug.TryFindComponentInChildren<Light>(gameObject);
            
            GrabInteractable = Helpers.Debug.TryFindComponent<XRGrabInteractable>(gameObject);
            TogglePower.AddListener(OnTogglePower);
            OnTogglePower(false);
        }

        public void Update()
        {
            Charge(gameObject.transform.position, Time.deltaTime);

            if (Active)
            {
                Battery -= Time.deltaTime;
            }

            if (Battery <= 0)
            {
                OnTogglePower(false);
            }
        }


    }
}

