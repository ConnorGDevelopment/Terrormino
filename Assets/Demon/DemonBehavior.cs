using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBehavior : MonoBehaviour
{

    [SerializeField] private Material _hook;
    [SerializeField] private Material _hookBase;
    [SerializeField] private Material _mainBody;
    [SerializeField] private Material _cube;

    //[SerializeField] private GameObject Demon;

    [SerializeField] private FlashlightTrigger _flashlightTrigger;

    private bool _isDissolving = false;

    private float _dissolveValue = 0f;

    void Start()
    {
        
    }


    void Update()
    {
        
        if (_flashlightTrigger.SpookEnemy == true)
        {
            _isDissolving = true;
            Debug.Log("should be dissolved");

            if (_isDissolving == true)
            {
                _dissolveValue += Time.deltaTime * 0.5f;

                _hook.SetFloat("Clipping_value", _dissolveValue);
                _hookBase.SetFloat("Clipping_value", _dissolveValue);
                _mainBody.SetFloat("Clipping_value", _dissolveValue);
                _cube.SetFloat("Clipping_value", _dissolveValue);

            }
            

        }

    }








}
