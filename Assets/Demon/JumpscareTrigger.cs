using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class JumpscareTrigger : MonoBehaviour
{
    public AudioSource Scream;
    
    public GameObject JumpscareDemon;

    private Player.Manager _playerManager;
    private Demon.Manager _demonManager;


    public Light MoonLight;
    

    private void Start()
    {


        

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Enemy"))
    //    {
    //        Jumpscare.Invoke();
    //        other.gameObject.SetActive(false);
    //    }
    //}



    public UnityEvent Jumpscare;
    public void OnJumpscare()
    {
        AdjustingMoonlight();
        _demonManager.Demons.ForEach(demon => demon.SetActive(false));
        Scream.Play();
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


    public void AdjustingMoonlight()
    {
       
        
        MoonLight.enabled = false;
        
        
    }


    public void Awake()
    {
        _playerManager = Helpers.Debug.TryFindByTag("Player").GetComponent<Player.Manager>();
        if (_playerManager != null)
        {
            _playerManager.GameOver.AddListener(OnJumpscare);
        }
        _demonManager = Helpers.Debug.TryFindByTag("DemonManager").GetComponent<Demon.Manager>();
    }



}
