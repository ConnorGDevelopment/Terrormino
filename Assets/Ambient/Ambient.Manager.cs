using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ambient
{
    public class Manager : MonoBehaviour
    {
        public List<Effect> Effects;
        public List<GameObject> TrackedObjects;
        public Collider PlayerVision;

        private float _timer = 0f;

        public List<GameObject> ValidObjects
        {
            get
            {
                return TrackedObjects
                    // Where returns all items of a list *where* a condition is true
                    .Where(
                        // param =>  
                        trackedObject =>
                            !PlayerVision
                            .bounds
                            .Intersects(
                                trackedObject
                                .GetComponent<Collider>()
                                .bounds
                                )
                            )
                    .ToList();
            }
        }

        public Dictionary<>

        public void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= 5f)
            {
                TriggerEffects();
                _timer = 0f;
            }
        }

        public void Start()
        {
            Effects.AddRange(gameObject.GetComponents<Effect>());
        }

        public void TriggerEffects()
        {
            foreach (Effect effect in Effects)
            {
                effect.TriggerEffect.Invoke(ValidObjects);
            }
        }



    }
}