using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Object
{
    public class AmbientObjectChange : MonoBehaviour
    {
        [System.Serializable]
        public class AmbientObject
        {
            public Transform spawnPoint;
            [HideInInspector] public GameObject currentInstance;
        }

        public List<GameObject> AmbientPrefabs = new(); // Possible object models
        public List<AmbientObject> AmbientObjects = new(); // Spawn points

        private Dictionary<GameObject, Coroutine> runningCoroutines = new();

        void Start()
        {
            foreach (var ambient in AmbientObjects)
            {
                // Spawn an initial object immediately
                ReplaceObject(ambient);

                // Start the object's change coroutine
                StartChangeCoroutine(ambient);
            }
        }

        private void StartChangeCoroutine(AmbientObject ambient)
        {
            GameObject key = ambient.spawnPoint.gameObject;

            if (!runningCoroutines.ContainsKey(key) || runningCoroutines[key] == null)
            {
                Coroutine c = StartCoroutine(ChangeRoutine(ambient));
                runningCoroutines[key] = c;
            }
        }

        private IEnumerator ChangeRoutine(AmbientObject ambient)
        {
            GameObject key = ambient.spawnPoint.gameObject;

            while (true)
            {
                float waitTime = Random.Range(5f, 10f); // Change to 45–120 in production
                yield return new WaitForSeconds(waitTime);

                ReplaceObject(ambient);
                Debug.Log($"Changed object at {ambient.spawnPoint.name}");
            }
        }

        private void ReplaceObject(AmbientObject ambient)
        {
            if (ambient.currentInstance != null)
                Destroy(ambient.currentInstance);

            GameObject newPrefab = AmbientPrefabs[Random.Range(0, AmbientPrefabs.Count)];
            ambient.currentInstance = Instantiate(newPrefab, ambient.spawnPoint.position, ambient.spawnPoint.rotation, ambient.spawnPoint);
        }

        public void SetObjectFrozen(GameObject spawnPointObj, bool isFrozen)
        {
            if (!runningCoroutines.ContainsKey(spawnPointObj))
                return;

            if (isFrozen)
            {
                if (runningCoroutines[spawnPointObj] != null)
                {
                    StopCoroutine(runningCoroutines[spawnPointObj]);
                    runningCoroutines[spawnPointObj] = null;
                    Debug.Log($"Froze object at {spawnPointObj.name}");
                }
            }
            else
            {
                if (runningCoroutines[spawnPointObj] == null)
                {
                    AmbientObject ambient = AmbientObjects.Find(a => a.spawnPoint.gameObject == spawnPointObj);
                    if (ambient != null)
                    {
                        Coroutine newCoroutine = StartCoroutine(ChangeRoutine(ambient));
                        runningCoroutines[spawnPointObj] = newCoroutine;
                        Debug.Log($"Unfroze object at {spawnPointObj.name}");
                    }
                }
            }
        }
    }
}
