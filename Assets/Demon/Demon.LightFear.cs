using UnityEngine;
using UnityEngine.Events;

namespace Demon
{
    public class LightFear : MonoBehaviour
    {
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

        private SkinnedMeshRenderer[] _skinnedMeshRenderers;

        public UnityEvent<bool> Illuminate = new();
        public void Dissolve(bool _)
        {
            foreach (var skinnedMeshRenderer in _skinnedMeshRenderers)
            {
                Debug.Log(Health / MaxHealth);
                skinnedMeshRenderer.material.SetFloat(Shader.PropertyToID("_DissolveValue"), Mathf.Clamp01(1 - (Health / MaxHealth)));
            }
        }

        public UnityEvent Banish = new();
        public void OnBanish()
        {
            Destroy(gameObject);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Flashlight"))
            {
                Health -= Time.deltaTime;
                Illuminate.Invoke(true);
                if (Health <= 0)
                {
                    Banish.Invoke();
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

        public void Start()
        {
            _skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
            Illuminate.AddListener(Dissolve);
            Banish.AddListener(OnBanish);
        }
    }
}

