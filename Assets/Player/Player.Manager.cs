using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class Manager : MonoBehaviour
    {
        public UnityEvent GameOver = new();

        public static void OnGameOver()
        {
            Debug.Log("Game Over");
        }

        public void Start()
        {
            GameOver.AddListener(OnGameOver);
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                Debug.Log(other.gameObject);
                GameOver.Invoke();   
            }
        }
    }
}