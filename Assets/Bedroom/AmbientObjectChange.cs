using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Object
{
    public class ChangeManager : MonoBehaviour
    {
        private LookDirection _playerLookDirection;

        public List<GameObject> AmbientObjects = new List<GameObject>();

        private float _changeTimer;


        public UnityEvent<bool> ObjectFreeze = new();

        void Start()
        {

            _playerLookDirection = Helpers.Debug.TryFindComponent<Player.LookDirection>(gameObject);

            ObjectFreeze.AddListener(StartCoroutine);

            

            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        IEnumerator ObjectChange()
        {
            foreach (GameObject obj in AmbientObjects)
            {
                _changeTimer = Random.Range(45, 120); //might need to track this in update??? dont want the value to change everytime the coroutine is run
                //TODO: Add in a way to change the timer PER object
                if(_changeTimer == 0)
                {
                    //TODO: Add in a way for the object to switch the model, i.e. color, rotation, position etc. 
                    Debug.Log("gulp");
                }
            }


            yield return null;
        }



        public void StopCoroutine()
        {
            StopAllCoroutines();
        }


        public void StartCoroutine(bool value)
        {
            if (value)
            {
                StartCoroutine(ObjectChange());
            }
            else
            {
                StopCoroutine();
            }
        }


    }
}

