using UnityEngine;

namespace Flashlight
{
    public class Trigger : MonoBehaviour
    {
        [SerializeField] private Shake _flashlightscript;

        [SerializeField] private Animator _animator;

        public float LightTimer = 3f;

        private bool _lightOnEnemy = false;
        public bool SpookEnemy = false;



        private void Update()
        {
            if (_lightOnEnemy)
            {
                LightTimer -= Time.deltaTime;

                if (LightTimer <= 0)
                {
                    SpookEnemy = true;
                }

            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (_flashlightscript.FlashlightActive && other.CompareTag("Enemy"))
            {
                _lightOnEnemy = true;
            }
        }




        private void OnTriggerStay(Collider other)
        {
            if (_flashlightscript.FlashlightActive && SpookEnemy)
            {
                Debug.Log("RAAAAAAAAAHHHH");
                _animator.SetBool("SpookEnemy", true);
                LightTimer = 3f;
                SpookEnemy = false;
            }
        }



        private void OnTriggerExit(Collider other)
        {
            if (_flashlightscript.FlashlightActive && other.CompareTag("Enemy"))
            {
                _lightOnEnemy = false;
                SpookEnemy = false;
                LightTimer = 3f;
            }
        }


    }
}