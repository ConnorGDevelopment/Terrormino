using System.Collections.Generic;
using UnityEngine;

namespace Ambient
{
    public class Manager : MonoBehaviour
    {
        public List<Effect> Effects;
        public List<GameObject> TrackedObjects;
        public Collider PlayerVision;

        //public float Frequency;
        //private float _timer = 0;
        //public void Update()
        //{
        //    _timer += Time.deltaTime;
        //    if (_timer >= Frequency)
        //    {
        //        var randomIndex = UnityEngine.Random.Range(0, Effects.Count);
        //        Effects[randomIndex].TriggerEffect.Invoke();
        //        _timer = 0;
        //    }
        //}
        public void Update()
        {
            List<GameObject> validObjects = new List<GameObject>();

            foreach (GameObject trackedObject in TrackedObjects)
            {
                if (!PlayerVision.bounds.Intersects(trackedObject.GetComponent<Collider>().bounds))
                {
                    validObjects.Add(trackedObject);
                }
            }

            foreach (Effect effect in Effects)
            {
                effect.TriggerEffect.Invoke(validObjects);
            }
        }

        public void Start()
        {
            Effects.AddRange(gameObject.GetComponents<Effect>());
        }
    }
}