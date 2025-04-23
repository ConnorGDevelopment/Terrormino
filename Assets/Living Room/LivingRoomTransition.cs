using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;




public class LivingRoomTransition : MonoBehaviour
{

    private float _transitionTime = 10;
    private bool _beginTransition = false;

    public Light LightSource;
    


    //Shader stuff
    public List<Material> materials = new List<Material>();


    private bool _isDissolving = false;
    private float _dissolveValue = 0f;


    public GameObject GameConsole;


    // Start is called before the first frame update
    public void Start()
    {
        _skinnedMeshRenderers = Helpers.Debug.TryFindComponentsInChildren<SkinnedMeshRenderer>(gameObject);

    }



    // Update is called once per frame
    public void Update()
    {
        if (_beginTransition)
        {

            if (_isDissolving)
            {
                DissolveConsole();
            }

            LightSource.intensity -= Time.deltaTime * 0.75f;

            
            _transitionTime -= Time.deltaTime;

            if (_transitionTime <= 0)
            {
                BeginGame();
                _transitionTime = 5;

                foreach (var skinnedMeshRenderer in _skinnedMeshRenderers)
                {
                    skinnedMeshRenderer.material.SetFloat("_DissolveValue", _dissolveValue);
                }
            }
        }
    }



    public void BeginGame()
    {

        SceneManager.LoadScene("Expo");
    }


    public UnityEvent<InputAction> OnTitleTransitionGrab = new();

    public void TitleToGameplayTransition(SelectEnterEventArgs context)
    {
        _beginTransition = true;
        _isDissolving = true;


        XRGrabInteractable grabInteractable = gameObject.GetComponent<XRGrabInteractable>();


        grabInteractable.interactionManager.SelectExit(grabInteractable.interactorsSelecting[0], grabInteractable);

        

    }


    private SkinnedMeshRenderer[] _skinnedMeshRenderers;

    public void DissolveConsole()
    {

        _dissolveValue += Time.deltaTime * 1f;
        _dissolveValue = Mathf.Clamp01(_dissolveValue); // Keep between 0 and 1

        foreach (var skinnedMeshRenderer in _skinnedMeshRenderers)
        {
            skinnedMeshRenderer.material.SetFloat("_DissolveValue", _dissolveValue);
        }


        if (_dissolveValue >= 1)
        {
            _isDissolving = false;
        }
    }





}




