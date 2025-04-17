using Object;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Player
{
    public class LookDirection : MonoBehaviour
    {

        private ChangeManager _changeManager;
        public Collider PlayerLookDirection;
        

        // Start is called before the first frame update
        void Start()
        {

            _changeManager = Helpers.Debug.TryFindComponent<Object.ChangeManager>(gameObject);

        }

        // Update is called once per frame
        void Update()
        {

        }


        public void OnTriggerStay(Collider other)
        {
            ObjectLookedAt(other.gameObject);
        }


        public void OnTriggerExit(Collider other)
        {
            _changeManager.ObjectFreeze.Invoke(false);
        }


        public void ObjectLookedAt(GameObject other)
        {
            _changeManager.ObjectFreeze.Invoke(true);
        }



    }
}
