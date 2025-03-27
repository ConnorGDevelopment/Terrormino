using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlashlightTrigger : MonoBehaviour
{
    [SerializeField] private FlashlightScript _flashlightscript;

    [SerializeField] private Animator _animator;

    public float LightTimer = 3f;

    private bool LightOnEnemy = false;
    public bool SpookEnemy = false;



    private void Update()
    {
        if(LightOnEnemy == true)
        {
            LightTimer -= Time.deltaTime;

            if(LightTimer <= 0)
            {
                SpookEnemy = true;
            }

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(_flashlightscript.FlashlightActive == true)
        {
            if (other.tag == "Enemy")
            {
                LightOnEnemy = true;
                
            }
        }
    }




    private void OnTriggerStay(Collider other)
    {
        if(_flashlightscript.FlashlightActive == true)
        {
            if (SpookEnemy == true)
            {
                Debug.Log("RAAAAAAAAAHHHH");
                _animator.SetBool("SpookEnemy", true);
                LightTimer = 3f;
                SpookEnemy = false;


            }
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if(_flashlightscript.FlashlightActive == true)
        {
            if(other.tag == "Enemy")
            {
                LightOnEnemy = false;
                SpookEnemy = false;
                LightTimer = 3f;
            }
        }
    }


}
