using UnityEngine;
using UnityEngine.Events;

namespace Ambient
{
    // The "{ code goes here }" part of a function/class/etc is called its 'implementation'
    // The 'abstract' keyword basically passes the buck of writing the implementation to child classes (classes that inherit this class)
    // Abstract classes don't get used directly, they can only be inherited by other classes
    public abstract class Effect : MonoBehaviour
    {
        // We trigger an internally used UnityEvent so that we can separate out calling the effect and the implementation of the effect
        public UnityEvent TriggerEffect = new();

        // Basic Timer
        public float Frequency;
        private float _timer = 0;
        public void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= Frequency)
            {
                TriggerEffect.Invoke();
            }
        }

        // By making an abstract method, we guarantee that all child classes will have an OnTriggerEffect method
        public abstract void OnTriggerEffect();

        // The 'virtual' keyword means this method can be replaced in a child class using the 'override' keyword
        // The reason we use virtual here instead of abstract is because we're providing some implementation
        // This lets child classes call 'base.OnEnable()' and perform this implementation and then go about doing their own implementation
        public virtual void OnEnable()
        {
            TriggerEffect.AddListener(OnTriggerEffect);
        }

    }
}
