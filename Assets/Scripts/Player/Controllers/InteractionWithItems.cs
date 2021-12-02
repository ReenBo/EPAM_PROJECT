using ET.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Player
{
    public class InteractionWithItems : MonoBehaviour, ICommand
    {
        IActivatable _activatable;

        protected void OnTriggerEnter(Collider collider)
        {
            _activatable = collider.GetComponent<IActivatable>();
        }

        public void ExecuteCommand()
        {
            if (_activatable != null)
            {
                _activatable.Activate();
            }
        }
    }
}
