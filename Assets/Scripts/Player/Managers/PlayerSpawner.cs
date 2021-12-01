using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Player.Spawner
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] GameObject _player;

        public GameObject Create(Transform target)
        {
            var player = Instantiate(_player, target.position, Quaternion.identity);
            return player;
        }

        public PlayerController CreatePlayerInSession(Transform target)
        {
            return Instantiate(_player, target.position, Quaternion.identity).
                GetComponent<PlayerController>();
        }

        public void CreatePlayerInSession(Vector3 point)
        {
            Instantiate(_player, point, Quaternion.identity);
        }
    }
}
