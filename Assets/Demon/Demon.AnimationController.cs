using UnityEngine;

namespace Demon
{
    public class AnimationController : MonoBehaviour
    {
        private LightFear _lightFear;
        private Animator _animator;

        public void OnBanish()
        {
            _animator.SetTrigger("Banish");
        }
        public void OnIlluminate()
        {
            _animator.SetBool("IsIlluminated", true);
        }
        public void OnJumpscare()
        {
            _animator.SetTrigger("Jumpscare");
        }

        public void Start()
        {
            _lightFear = Helpers.Debug.TryFindComponent<LightFear>(gameObject);
            if (_lightFear != null)
            {
                _lightFear.Banish.AddListener(OnBanish);
                _lightFear.Illuminate.AddListener(OnIlluminate);
            }

            _animator = Helpers.Debug.TryFindComponent<Animator>(gameObject);
        }
    }
}
