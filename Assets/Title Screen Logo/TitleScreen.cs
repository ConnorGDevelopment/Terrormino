
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using Helpers;




public class TitleScreen : MonoBehaviour
{

    private float _transitionTime = 10;
    private bool _beginTransition = false;

    public Light LightSource;
    public ParticleSystem Fog;


    //Shader stuff
    public List<Material> materials = new List<Material>();


    private bool _isDissolving = false;
    private float _dissolveValue = 0f;


    private int _shaderRef = Shader.PropertyToID("_clipping_value");

    public GameObject GameConsole;


    // Start is called before the first frame update
    public void Start()
    {
      
    }



    // Update is called once per frame
    public void Update()
    {
        if(_beginTransition == true)
        {

            if(_isDissolving)
            {
                DissolveConsole();
            }

            LightSource.intensity -= Time.deltaTime * 0.75f;

            var emission = Fog.emission;
            emission.rateOverTime = Mathf.Max(0, emission.rateOverTime.constant - Time.deltaTime * 15f);

            _transitionTime -= Time.deltaTime;

            if(_transitionTime <= 0)
            {
                BeginGame();
                _transitionTime = 5;

                foreach (Material mat in materials)
                {
                    mat.SetFloat(_shaderRef, 0);
                }
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
            _isDissolving = true;


            XRGrabInteractable grabInteractable = GameConsole.GetComponent<XRGrabInteractable>();
           
            
            grabInteractable.interactionManager.SelectExit(grabInteractable.interactorsSelecting[0], grabInteractable);

            Collider ConsoleCollider = GameConsole.GetComponent<BoxCollider>();

            ConsoleCollider.enabled = false;
        }
        
    }



    public void DissolveConsole()
    {

        _dissolveValue += Time.deltaTime * 1f;
        _dissolveValue = Mathf.Clamp01(_dissolveValue); // Keep between 0 and 1

        foreach (Material mat in materials)
        {
            mat.SetFloat(_shaderRef, _dissolveValue);
        }


        if (_dissolveValue >= 1)
        {
            _isDissolving = false;
        }
    }





}


    

