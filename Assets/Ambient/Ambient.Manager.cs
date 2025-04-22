using System.Collections.Generic;
using UnityEngine;

namespace Ambient
{
    public class Manager : MonoBehaviour
    {
        public List<Effect> Effects;

        public float Frequency;
        private float _timer = 0;
        public void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= Frequency)
            {
                var randomIndex = UnityEngine.Random.Range(0, Effects.Count);
                Effects[randomIndex].TriggerEffect.Invoke();
            }
        }

        public void Start()
        {
            Effects.AddRange(gameObject.GetComponents<Effect>());
        }
    }
}