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


        private SkinnedMeshRenderer[] _skinnedMeshRenderers;
        public void OnIlluminate(bool value)
        {
            _animator.SetBool("IsIlluminated", value);
            // This sets the % of dissolve to the Demon's health, so it doesn't actually matter if is or isn't being illuminated
            foreach (var skinnedMeshRenderer in _skinnedMeshRenderers)
            {
                skinnedMeshRenderer.material.SetFloat(Shader.PropertyToID("_DissolveValue"), Mathf.Clamp01(1 - (_lightFear.Health / _lightFear.MaxHealth)));
            }
        }

        public AudioSource JumpscareScream;
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
            _skinnedMeshRenderers = Helpers.Debug.TryFindComponentsInChildren<SkinnedMeshRenderer>(gameObject);

            Helpers.Debug.CheckIfSetInInspector(gameObject, JumpscareScream, "JumpscareScream");
        }
    }
}
