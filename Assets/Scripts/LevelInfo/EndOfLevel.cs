using ET.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class EndOfLevel : MonoBehaviour
    {
        private IScenesManager _scenesManager;

        public void Init(IScenesManager scenesManager)
        {
            _scenesManager = scenesManager;
        }

        protected void OnTriggerEnter(Collider collider)
        {
            var player = collider.GetComponent<IPlayer>();

            if (player != null)
            {
                _scenesManager.NextLevel();
            }
        }
    }
}
