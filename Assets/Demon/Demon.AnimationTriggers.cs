using UnityEngine;

namespace Demon
{
    public class AnimationController : MonoBehaviour
    {
        public LightFear _lightFear;
        public Animator _animator;
        public AudioSource _scream;
        private Demon.Manager _demonManager;
        public Player.Manager _playerManager;
        private float _dissolveValue = 0f;

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
                Dissolve();
            }
        }


        //public void OnJumpscare()
        //{
        //    var cameraPosition = _playerManager.GetComponentInChildren<Camera>().gameObject.transform;
        //    gameObject.transform.SetParent(cameraPosition, false);
        //    gameObject.transform.localPosition = new Vector3(0f, 0f, 3.8f);
        //    gameObject.transform.LookAt(cameraPosition);
        //    _animator.SetTrigger("Jumpscare");
        //    _scream.Play();
        //    StartCoroutine(EndJumpscare());
        //}
        //IEnumerator EndJumpscare()
        //{
        //    yield return new WaitForSeconds(1.5f);
        //    _scream.Stop();
        //    _playerManager.BackToTitle();
        //}

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
        }

        public void Dissolve()
        {
            _dissolveValue += Time.deltaTime * 1f;
            _dissolveValue = Mathf.Clamp01(_dissolveValue); // Keep between 0 and 1

            foreach (var skinnedMeshRenderer in _skinnedMeshRenderers)
            {
                skinnedMeshRenderer.material.SetFloat("_DissolveValue", _dissolveValue);
            }
        }
    }
}
