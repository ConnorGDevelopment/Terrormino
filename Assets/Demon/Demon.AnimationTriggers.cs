using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Demon
{
    public class AnimationController : MonoBehaviour
    {
        private LightFear _lightFear;
        private Animator _animator;
        private AudioSource _scream;
        private Light _moonlight;
        private Demon.Manager _demonManager;
        private Player.Manager _playerManager;
        public void OnBanish(GameObject _)
        {
            _animator.SetTrigger("Banish");
        }


        private SkinnedMeshRenderer[] _skinnedMeshRenderers;
        public void OnIlluminate(bool value)
        {
            _animator.SetBool("IsIlluminated", value);

            if ((_lightFear.Health / _lightFear.MaxHealth) <= 1f / 8)
            {
                foreach (var skinnedMeshRenderer in _skinnedMeshRenderers)
                {
                    skinnedMeshRenderer.material.SetFloat(Shader.PropertyToID("_DissolveValue"), Mathf.Clamp(1 - (_lightFear.Health / _lightFear.MaxHealth), 0, 1f));
                }
            }

        }

        

        public void Awake()
        {
            _lightFear = Helpers.Debug.TryFindComponent<LightFear>(gameObject);
            if (_lightFear != null)
            {
                _lightFear.Banish.AddListener(OnBanish);
                _lightFear.Illuminate.AddListener(OnIlluminate);
            }

            //_playerManager = Helpers.Debug.TryFindByTag("Player").GetComponent<Player.Manager>();
            //if (_playerManager != null)
            //{
            //    _playerManager.GameOver.AddListener(OnJumpscare);
            //}

            _animator = Helpers.Debug.TryFindComponent<Animator>(gameObject);
            _skinnedMeshRenderers = Helpers.Debug.TryFindComponentsInChildren<SkinnedMeshRenderer>(gameObject);
            _demonManager = Helpers.Debug.TryFindByTag("DemonManager").GetComponent<Demon.Manager>();
            _scream = Helpers.Debug.TryFindComponent<AudioSource>(gameObject);
            _moonlight = Helpers.Debug.TryFindByTag("Moonlight").GetComponent<Light>();
        }
    }
}
