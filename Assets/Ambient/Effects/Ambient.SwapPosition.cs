using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ambient
{
    public class SwapPosition : Effect
    {
        public List<Transform> SpawnPoints;
        public float minDelay = 5f; 
        public float maxDelay = 10f; 

        private List<Transform> _availableSpawnPoints;
        private HashSet<GameObject> _currentlySwapping = new();
        public Collider PlayerVision;

        public override void OnEnable()
        {
            base.OnEnable();
        }

        public override void OnTriggerEffect(List<GameObject> gameObjects)
        {
            
            _availableSpawnPoints = new List<Transform>(SpawnPoints);

            foreach (var obj in gameObjects)
            {
                StartCoroutine(SwapAfterDelay(obj));
            }
        }

        private IEnumerator SwapAfterDelay(GameObject obj)
        {
            if (_currentlySwapping.Contains(obj))   //Will stop the swap if the player is looking at the obj
                yield break;

            _currentlySwapping.Add(obj);

            float delay = Random.Range(minDelay, maxDelay);
            float elapsed = 0f;

            
            while (elapsed < delay)
            {
                if (PlayerVision.bounds.Intersects(obj.GetComponent<Collider>().bounds))
                {
                    _currentlySwapping.Remove(obj); 
                    yield break;
                }

                elapsed += Time.deltaTime;
                yield return null;
            }

            if (_availableSpawnPoints.Count == 0)
            {
                _currentlySwapping.Remove(obj);
                yield break;
            }

            int randomIndex = Random.Range(0, _availableSpawnPoints.Count);
            Transform chosenSpawn = _availableSpawnPoints[randomIndex];
            obj.transform.position = chosenSpawn.position;

            _availableSpawnPoints.RemoveAt(randomIndex);
            _currentlySwapping.Remove(obj);
        }
    }
}
