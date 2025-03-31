using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpscareTrigger : MonoBehaviour
{
    public AudioSource Scream;
    
    public GameObject Jumpscare;
    



    public Light MoonLight;
    public Light GameConsoleLight;
    public Light Flashlight;


    private void Start()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {

            Debug.Log("jumpscare");
            Scream.Play();
            Jumpscare.SetActive(true);
            AdjustingMoonlight();
          
            StartCoroutine(EndJumpscare());
        }        
    }

    IEnumerator EndJumpscare()
    {
        yield return new WaitForSeconds(1.5f);
        Scream.Stop();
        Jumpscare.SetActive(false);
        SceneManager.LoadScene("TitleScreen");
        

    }


    public void AdjustingMoonlight()
    {
        while (MoonLight.intensity < 0.3)
        {
            MoonLight.enabled = false;
            GameConsoleLight.enabled = false;
            Flashlight.enabled = false;
        }
    }




}
