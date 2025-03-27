using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpscareTrigger : MonoBehaviour
{
    public AudioSource Scream;
    //public GameObject PlayerCam;
    public GameObject Jumpscare;
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {

            Debug.Log("jumpscare");
            Scream.Play();
            Jumpscare.SetActive(true);
            //PlayerCam.SetActive(false);
          
            StartCoroutine(EndJumpscare());
        }        
    }

    IEnumerator EndJumpscare()
    {
        yield return new WaitForSeconds(2.03f);
        //PlayerCam.SetActive(true);
        Jumpscare.SetActive(false);
        

    }




}
