using UnityEngine;

public class DemonBehavior : MonoBehaviour
{
    [SerializeField] private Material _hook;
    [SerializeField] private Material _hookBase;
    [SerializeField] private Material _mainBody;
    [SerializeField] private Material _cube;

    [SerializeField] private FlashlightTrigger _flashlightTrigger;

    private bool _isDissolving = false;
    private float _dissolveValue = 0f;


    private int _shaderRef = Shader.PropertyToID("_Clipping_value");

    void Start()
    {

        // TODO: create a way for the shaders to reset themselves without hard coding it in

    }

    void Update()
    {

        if (_flashlightTrigger.SpookEnemy)
        {
            _isDissolving = true;


        }
        if (_isDissolving)
        {
            _dissolveValue += Time.deltaTime * 1f;
            _dissolveValue = Mathf.Clamp01(_dissolveValue); // Keep between 0 and 1

            _hook.SetFloat(_shaderRef, _dissolveValue);
            _hookBase.SetFloat(_shaderRef, _dissolveValue);
            _mainBody.SetFloat(_shaderRef, _dissolveValue);
            _cube.SetFloat(_shaderRef, _dissolveValue);

            Debug.Log($"Dissolve Value: {_dissolveValue}");
            Debug.Log($"Hook Material: {_hook.GetFloat(_shaderRef)}");





            if (_dissolveValue >= 1f)
            {
                    _isDissolving = false; // Stop dissolving once complete
            }
            
        }
    }
}
