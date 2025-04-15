using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Player
{
    public class Manager : MonoBehaviour
    {
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
            GameOver.AddListener(OnGameOver);
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Demon.LightFear _))
            {
                Debug.Log(other.gameObject);
                //EditorApplication.isPaused = true;
                GameOver.Invoke();
            }
        }
    }
}