using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class JumpscareTrigger : MonoBehaviour
{
    public AudioSource Scream;

    public GameObject JumpscareDemon;
    public GameObject Room;

    private Player.Manager _playerManager;
    private Demon.Manager _demonManager;

    public Light MoonLight;

    public UnityEvent Jumpscare;

    public void OnJumpscare()
    {
        //AdjustingMoonlight();
        _demonManager.Demons.ForEach(demon => demon.SetActive(false));
        Scream.Play();
        JumpscareDemon.SetActive(true);
        Room.SetActive(false);

        StartCoroutine(EndJumpscare());
    }

    IEnumerator EndJumpscare()
    {
        yield return new WaitForSeconds(1.5f);
        Scream.Stop();
        JumpscareDemon.SetActive(false);
        SceneManager.LoadScene(_sceneName);
    }

    //public void AdjustingMoonlight()
    //{

    //    MoonLight.enabled = false;

    //}

    private string _sceneName;

    public void Awake()
    {
        _playerManager = Helpers.Debug.TryFindByTag("Player").GetComponent<Player.Manager>();
        if (_playerManager != null)
        {
            _playerManager.GameOver.AddListener(OnJumpscare);
        }
        _demonManager = Helpers.Debug.TryFindByTag("DemonManager").GetComponent<Demon.Manager>();
        if (gameObject.TryGetComponent(out ScenePicker scenePicker))
        {
            _sceneName = scenePicker.ScenePath;
        }
        else
        {
            Debug.Log($"No ScenePicker component found on {gameObject.name}", gameObject);
        }
    }
}
