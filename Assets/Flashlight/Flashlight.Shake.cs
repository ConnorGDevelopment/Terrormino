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

        public float _battery = 5f;
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
            LightCollider.enabled = Active;
        }
        public void OnTogglePower(bool value)
        {
            Active = value;
            LightSource.enabled = Active;
            LightCollider.enabled = Active;
        }

        private Vector3 _cachedPosition = Vector3.zero;
        private Vector3 _cachedVelocity = Vector3.zero;
        public void Charge(Vector3 position, float deltaTime)
        {
            if (GrabInteractable.isSelected && !Active)
            {
                var velocity = (position - _cachedPosition) / deltaTime;

                Debug.Log(Vector3.Angle(velocity, _cachedVelocity));

                if (Vector3.Angle(velocity, _cachedVelocity) > 5f)
                {
                    Battery += 1f;
                }

                _cachedVelocity = velocity;
                _cachedPosition = position;
            }
        }



        public void Start()
        {
            LightSource = Helpers.Debug.TryFindComponentInChildren<Light>(gameObject);
            Helpers.Debug.CheckIfSetInInspector(gameObject, LightCollider, "LightCollider");
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

