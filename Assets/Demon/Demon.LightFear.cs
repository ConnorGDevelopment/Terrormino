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
                _health = Mathf.Clamp(_health + value, 0, MaxHealth);
            }
        }

        private SkinnedMeshRenderer[] _skinnedMeshRenderers;

        public UnityEvent Illuminate = new();
        public void Dissolve()
        {
            foreach (var skinnedMeshRenderer in _skinnedMeshRenderers)
            {
                skinnedMeshRenderer.material.SetFloat(Shader.PropertyToID("_DissolveValue"), Mathf.Clamp01(Health / MaxHealth));
            }
        }

        public UnityEvent Banish = new();
        public void OnBanish()
        {
            Destroy(gameObject);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.TryGetComponent(out FlashlightShake _))
            {
                Health -= Time.deltaTime;
                Debug.Log(Health);
                Illuminate.Invoke();
                if (Health <= 0)
                {
                    Banish.Invoke();
                }
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

