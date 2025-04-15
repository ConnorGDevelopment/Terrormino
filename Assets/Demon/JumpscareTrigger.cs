using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class JumpscareTrigger : MonoBehaviour
{
    public AudioSource Scream;

    public GameObject JumpscareDemon;
    public GameObject Bedroom;


    private Player.Manager _playerManager;
    private Demon.Manager _demonManager;
    

    public Light MoonLight;


    private void Start()
    {

        


    }





    public UnityEvent Jumpscare = new();

    

    public void OnJumpscare()
    {
        
        _demonManager._demons.ForEach(demon => demon.SetActive(false));
        Scream.Play();
        Bedroom.SetActive(false);
        JumpscareDemon.SetActive(true);


        StartCoroutine(EndJumpscare());
    }


    IEnumerator EndJumpscare()
    {
        yield return new WaitForSeconds(1.5f);
        Scream.Stop();
        JumpscareDemon.SetActive(false);
        SceneManager.LoadScene("TitleScreen");
    }




    public void Awake()
    {
        _playerManager = Helpers.Debug.TryFindByTag("Player").GetComponent<Player.Manager>();
        if (_playerManager != null)
        {
            _playerManager.GameOver.AddListener(OnJumpscare);
        }
        _demonManager = Helpers.Debug.TryFindByTag("DemonManager").GetComponent<Demon.Manager>();

        Jumpscare.AddListener(OnJumpscare);
    }



}
