using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Player
{
    public class Manager : MonoBehaviour
    {
        public JumpscareTrigger Jumpscaretrigger;
        public UnityEvent GameOver = new();

        public void OnGameOver()
        {
            Debug.Log("Game Over");
        }

        public void BackToTitle()
        {
            SceneManager.LoadScene("TitleScreen");
        }

        public void Start()
        {

            Jumpscaretrigger = Helpers.Debug.TryFindComponentOnGameObjectByTag<JumpscareTrigger>("Player");

            GameOver.AddListener(OnGameOver);
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Demon.LightFear _))
            {
                Debug.Log(other.gameObject);
                //EditorApplication.isPaused = true;
                GameOver.Invoke();
                Jumpscaretrigger.Jumpscare.Invoke();
            }
        }
    }
}