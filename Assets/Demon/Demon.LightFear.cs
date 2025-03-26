using UnityEngine;

namespace Demon
{
    public class LightFear : MonoBehaviour
    {
        private SkinnedMeshRenderer[] _renderers = null;
        private float _dissolveValue = 0f;

        [HideInInspector]
        public float DissolveValue
        {
            get { return _dissolveValue; }
            set
            {
                _dissolveValue = Mathf.Clamp01(value);
            }
        }

        public void Start()
        {
            _renderers = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        }

        private void OnCollisionStay()
        {
            DissolveValue += Time.deltaTime;
            foreach (var renderer in _renderers)
            {
                renderer.material.SetFloat("_clippingValue", DissolveValue);
            }
        }
    }
}