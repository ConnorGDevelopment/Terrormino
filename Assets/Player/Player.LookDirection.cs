using Object;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class LookDirection : MonoBehaviour
    {
        private AmbientObjectChange _changeManager;
        private HashSet<GameObject> currentlyLookingAt = new();

        void Start()
        {
            _changeManager = Helpers.Debug.TryFindComponent<AmbientObjectChange>(gameObject);
        }

        //void OnTriggerEnter(Collider other)
        //{
        //    if (IsAmbientObject(other.gameObject))
        //    {
        //        if (!currentlyLookingAt.Contains(other.gameObject))
        //        {
        //            currentlyLookingAt.Add(other.gameObject);
        //            _changeManager.SetObjectFrozen(other.gameObject, true);
        //        }
        //    }
        //}

        private void OnTriggerStay(Collider other)
        {
            if (IsAmbientObject(other.gameObject))
            {
                if (!currentlyLookingAt.Contains(other.gameObject))
                {
                    currentlyLookingAt.Add(other.gameObject);
                    _changeManager.SetObjectFrozen(other.gameObject, true);
                }
            }
        }


        void OnTriggerExit(Collider other)
        {
            if (IsAmbientObject(other.gameObject))
            {
                if (currentlyLookingAt.Contains(other.gameObject))
                {
                    currentlyLookingAt.Remove(other.gameObject);
                    _changeManager.SetObjectFrozen(other.gameObject, false);
                }
            }
        }

        private bool IsAmbientObject(GameObject obj)
        {
            return obj.CompareTag("Ambient");
        }
    }
}
