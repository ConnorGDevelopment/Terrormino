using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonAnimationScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] FlashlightTrigger _flashlightTrigger;

    [SerializeField] Animator _animator;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_flashlightTrigger.SpookEnemy == true)
        {
            _animator.SetBool("SpookEnemy", true);
        }
    }
}
