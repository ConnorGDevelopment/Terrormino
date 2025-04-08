using UnityEngine;
using UnityEngine.AI;

namespace Demon
{
    public class Pathing : MonoBehaviour
    {
        private GameObject _player;
        private Vector3 _destination;
        private NavMeshAgent _agent;

        public void Start()
        {
            _player = Helpers.Debug.TryFindByTag("Player");
            _agent = Helpers.Debug.TryFindComponent<NavMeshAgent>(gameObject);
            if (_player && _agent)
            {
                _destination = _player.transform.position;
                _agent.destination = _destination;
            }
        }

        public static int IntFromMask(int mask)
        {
            for (int i = 0; i < 32; ++i)
            {
                if ((1 << i) == mask)
                    return i;
            }
            return -1;
        }

        private bool _enteredSlow = false;
        public void Update()
        {
            if (NavMesh.SamplePosition(gameObject.transform.position, out NavMeshHit hit, 5f, NavMesh.AllAreas) && IntFromMask(hit.mask) == NavMesh.GetAreaFromName("Slow") && !_enteredSlow)
            {
                _agent.velocity = (_agent.velocity / 4);
                _agent.speed = 1;
                _enteredSlow = true;
            }
        }
    }

}
