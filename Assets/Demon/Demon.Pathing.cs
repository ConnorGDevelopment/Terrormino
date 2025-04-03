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
    }

}
