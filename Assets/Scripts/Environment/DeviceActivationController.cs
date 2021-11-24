using ET.Interface;
using ET.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ET.Environment;
using ET.Environment.Door;

namespace ET.Device
{
    public class DeviceActivationController : MonoBehaviour, IActivatable
    {
        [SerializeField] private TypeTerminals _typeTerminal;
        [SerializeField] private DoorsController _door;
        [SerializeField] private Image _activatingButton;
        [SerializeField] private AudioClip _audioClipInteractionWithTerminal;
        [SerializeField] private AudioClip _audioClipError;

        private AudioSource _audioSource;

        private bool _isActivated = false;
        private bool _enabledTerminal = false;
        private bool _keyCardIsAvailable = false;

        //public event Action onOpeningDoorEvent;

        protected void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider collider)
        {
            var _playerController = collider.GetComponent<PlayerController>();

            if (_playerController != null)
            {
                _activatingButton.gameObject.SetActive(true);
                _isActivated = true;
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            var _playerController = collider.GetComponent<PlayerController>();

            if (_playerController != null)
            {
                _activatingButton.gameObject.SetActive(false);
                _isActivated = false;
            }
        }

        public void Activate()
        {
            if (_isActivated)
            {
                if (_typeTerminal != TypeTerminals.CORAL)
                {
                    StartCoroutine(TurnOnTerminal());
                }
                else if(_keyCardIsAvailable)
                {
                    StartCoroutine(TurnOnTerminal());
                }
                else
                {
                    _audioSource.PlayOneShot(_audioClipError);
                }
            }
        }

        private IEnumerator TurnOnTerminal() 
        {
            _audioSource.PlayOneShot(_audioClipInteractionWithTerminal);

            while(_activatingButton.fillAmount > 1e-2f)
            {
                _activatingButton.fillAmount -= 1e-2f;
                yield return null;
            }
            _enabledTerminal = true;

            _door.Open();

            //OpenDoor(_enabledTerminal);

            yield break;
        }

        //private void OpenDoor(bool enabled)
        //{
        //    if (enabled)
        //    {
        //        onOpeningDoorEvent.Invoke();
        //    }
        //}
    }
}
