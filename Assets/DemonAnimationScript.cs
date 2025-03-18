using UnityEngine;

public class DemonAnimationScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Flashlight.Trigger _flashlightTrigger;

    [SerializeField] private Animator _animator;



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
