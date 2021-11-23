using ET.Interface.IComand;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Device
{
    public class DeviceActivationController : MonoBehaviour, ICommand
    {
        private Transform _playerPosition;
        [SerializeField] private Image _activatingButton;

        private float _currentDistance;
        private float _allowedDistance = 2f;

        private bool _isActivated = false;
        private bool _enabledTerminal = false;

        public event Action onOpenDoorEvent;

        protected void Start()
        {
            _playerPosition = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;
        }

        protected void LateUpdate()
        {
            _currentDistance = Vector3.Distance(transform.position, _playerPosition.position);

            EnableTerminalPopups();
        }

        private void EnableTerminalPopups()
        {
            if (_currentDistance <= _allowedDistance)
            {
                _activatingButton.gameObject.SetActive(true);
                _isActivated = true;
            }
            else
            {
                _activatingButton.gameObject.SetActive(false);
                _isActivated = false;
            }
        }

        public void ExecuteCommand()
        {
            if (_isActivated)
            {
                StartCoroutine(ActivatesTerminal());
                OpenDoor(_enabledTerminal);
            }
        }

        private IEnumerator ActivatesTerminal()
        {
            while(_activatingButton.fillAmount > 1e-2f)
            {
                _activatingButton.fillAmount -= 1e-2f;
                yield return null;
            }
            _enabledTerminal = true;
            OpenDoor(_enabledTerminal);

            yield break;
        }

        private void OpenDoor(bool enabled)
        {
            if (enabled)
            {
                onOpenDoorEvent.Invoke();
            }
        }
    }
}
