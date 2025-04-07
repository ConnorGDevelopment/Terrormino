using UnityEngine;
using UnityEngine.Events;

namespace Demon
{
    public class LightFear : MonoBehaviour
    {
        // Health, uses a backing field to ensure that its only modified in a certain way
        public float MaxHealth = 3f;
        private float _health = 3f;
        public float Health
        {
            get { return _health; }
            set
            {
                _health = Mathf.Clamp(value, 0, MaxHealth);
            }
        }

        public UnityEvent<bool> Illuminate = new();
        // This happens every frame the Flashlight is intersecting the Demon
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Flashlight"))
            {
                var shake = other.GetComponentInParent<Flashlight.Shake>();
                if (shake.Active)
                {
                    Health -= Time.deltaTime;
                    Illuminate.Invoke(true);
                    if (Health <= 0)
                    {
                        Banish.Invoke(gameObject);
                    }
                }
                else
                {
                    Illuminate.Invoke(false);
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Flashlight"))
            {
                Illuminate.Invoke(false);
            }
        }

        // In case other things want to respond, the Demon being destroyed is wrapped in an event
        // When Banish is invoked, it sets a marker to destroy it in LateUpdate() which is the same as Update() except it runs after everything
        // EventListeners are executed in the order they're added, this basically ensure that the actual destroy runs after everything else
        public UnityEvent<GameObject> Banish = new();
        public void StartDelayedDestroy(GameObject _)
        {
            _destroyInLateUpdate = true;
        }
        private bool _destroyInLateUpdate = false;
        public void LateUpdate()
        {
            if (_destroyInLateUpdate)
            {
                //Destroy(gameObject);
            }
        }

        public void Start()
        {
            Banish.AddListener(StartDelayedDestroy);
        }

        // For seeing values in the inspector, can remove for production if desired
        public float PublicHealth;
        public void OnValidate()
        {
            PublicHealth = Health;
        }
    }
}

