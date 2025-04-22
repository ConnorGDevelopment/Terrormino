using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Ambient
{
    public class Manager : MonoBehaviour
    {
        public List<Effect> Effects;
        public List<GameObject> TrackedObjects;
        public Collider PlayerVision;

        private float timer = 0f; //this is a timer to check how frequently we can do effect stuff
                                  //without this, the effect stuff happens every frame if the logic is kept in update
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
            timer += Time.deltaTime;

            if (timer >= 5f)
            {
                TriggerEffects();
                timer = 0f;
            }

        }

        public void Start()
        {
            Effects.AddRange(gameObject.GetComponents<Effect>());
        }


        void TriggerEffects()
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



    }
}