using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlashlightTrigger : MonoBehaviour
{
    [SerializeField] private FlashlightShake _flashlightshake; //reference to the flashlight shake script

    [SerializeField] private Animator _animator;   //reference to the demons animator

    public float LightTimer = 3f;  //How long the light needs to be on the enemy to trigger a reaction

    private bool LightOnEnemy = false;  //bool for checking if the light is on the enemy
    public bool SpookEnemy = false;   //bool for if the enemy has triggered a reaction



    private void Update()
    {
        if(LightOnEnemy == true)            //Checking to see if the light is on the enemy
        {
            LightTimer -= Time.deltaTime;      //Subtracting the time the light needs to be on the enemy

            if(LightTimer <= 0)          
            {
                SpookEnemy = true;           //triggers a reaction if the timer hits 0
            }

        }
    }


    private void OnTriggerEnter(Collider other)      //When the demon enters the collider
    {
        if(_flashlightshake.FlashlightActive == true)     //If the flashlight is on
        {
            if (other.tag == "Enemy")              //Checks the tag of the demon
            {
                LightOnEnemy = true;        //Turns the bool of the light being on the enemy to being true
                
            }
        }
    }




    private void OnTriggerStay(Collider other)    //If the demon stays in the collider
    {
        if(_flashlightshake.FlashlightActive == true)        //If the flashlight is active
        {
            if (SpookEnemy == true)             //Checking to see if the enemy has triggered a reaction and if it has, it essentially resets everything
            {
                Debug.Log("RAAAAAAAAAHHHH");
                _animator.SetBool("SpookEnemy", true);
                LightTimer = 3f;
                SpookEnemy = false;


            }
        }
    }



    private void OnTriggerExit(Collider other)      //Checking to see if the demon has exited the collider
    {
        if(_flashlightshake.FlashlightActive == true)
        {
            if(other.tag == "Enemy")        
            {
                LightOnEnemy = false;
                SpookEnemy = false;              //If the demon leaves the collider it resets everything
                LightTimer = 3f;
            }
        }
    }


}
