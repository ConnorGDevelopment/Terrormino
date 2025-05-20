using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Demon
{
    public class Manager : MonoBehaviour
    {
        public List<GameObject> Demons = new();
        public GameObject DemonPrefab;
        public List<Collider> SpawnColliders = new();

        public Helpers.Timer GraceTimer = new(30f);
        public Helpers.Timer SpawnTimer = new(15f);

        public void Start()
        {
            GraceTimer.StartTimer();
            SpawnTimer.StartTimer();
        }

        public void OnBanish(GameObject demon)
        {
            Demons.Remove(demon);
        }

        public void SpawnDemon()
        {
            if (Demons.Count == 0)
            {
                var selectedSpawnCollider = SpawnColliders[Random.Range(0, SpawnColliders.Count)];
                Bounds spawnBounds = new(
                    selectedSpawnCollider.bounds.center,
                    selectedSpawnCollider.bounds.size
                );

                if (DemonPrefab.TryGetComponent(out Collider demonCollider))
                {
                    spawnBounds.Expand(-demonCollider.bounds.extents);
                    Vector3 spawnLocation = new();
                    spawnLocation.x = Random.Range(-spawnBounds.extents.x, spawnBounds.extents.x);
                    spawnLocation.y = 0;
                    spawnLocation.z = Random.Range(-spawnBounds.extents.z, spawnBounds.extents.z);

                    if (
                        NavMesh.SamplePosition(
                            spawnLocation + spawnBounds.center,
                            out NavMeshHit hit,
                            5f,
                            NavMesh.AllAreas
                        )
                    )
                    {
                        Debug.Log("Hit");
                        var demon = Instantiate(DemonPrefab, hit.position, Quaternion.identity);
                        demon.GetComponent<Demon.LightFear>().Banish.AddListener(OnBanish);
                        Demons.Add(demon);
                    }
                }
            }
        }

        public void ClearAll()
        {
            Demons.ForEach(demon => demon.GetComponent<Demon.LightFear>().Banish.Invoke(demon));
        }

        public void Update()
        {
            if (GraceTimer.Ringing && SpawnTimer.Ringing)
            {
                SpawnDemon();
                SpawnTimer.RestartTimer();
            }
        }
    }
}
