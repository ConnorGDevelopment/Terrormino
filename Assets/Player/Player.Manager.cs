using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEditor;
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
            if (other.TryGetComponent(out Demon.LightFear _))
            {
                Debug.Log(other.gameObject);
                //EditorApplication.isPaused = true;
                GameOver.Invoke();
            }
        }
    }
}