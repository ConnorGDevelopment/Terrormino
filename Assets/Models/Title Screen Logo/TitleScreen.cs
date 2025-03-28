using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using Helpers;
using UnityEngine.Android;



public class TitleScreen : MonoBehaviour
{

    private float _transitionTime = 10;
    private bool _beginTransition = false;

    public Light LightSource;
    public ParticleSystem Fog;
    public MeshRenderer StartObjectRenderer;


    // Start is called before the first frame update
    public void Start()
    {
        
    }



    // Update is called once per frame
    public void Update()
    {
        if(_beginTransition == true)
        {
            LightSource.intensity -= Time.deltaTime * 0.75f;

            var emission = Fog.emission;
            emission.rateOverTime = Mathf.Max(0, emission.rateOverTime.constant - Time.deltaTime * 15f);

            _transitionTime -= Time.deltaTime;

            if(_transitionTime <= 0)
            {
                BeginGame();
                _transitionTime = 5;
            }
        }
    }
    
     

    public void BeginGame()
    {
        SceneManager.LoadScene("Flashlight and Handheld Grabbing");
    }


    public UnityEvent<InputAction> OnTitleTransitionGrab = new();

    public void TitleToGameplayTransition(InputAction inputAction)
    {
        int triggerInput = Math.RoundNearestNonZeroInt(inputAction.ReadValue<float>());
        UnityEngine.Debug.Log(triggerInput);
        if (triggerInput == 1)
        {
            _beginTransition = true;
            StartObjectRenderer.enabled = false;
        }
        
    }
}


    

