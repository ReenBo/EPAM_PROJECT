using ET.Device;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ET.Environment.Door
{
    public class DoorsController : MonoBehaviour
    {
        [SerializeField] private GameObject _leftDoor;  /// -3.5
        [SerializeField] private GameObject _RightDoor; ///  3.5 
        [SerializeField] private DeviceActivationController _device;
        [SerializeField] private AudioClip _audioClipOpenDoor;

        private AudioSource _audioSource;

        Vector3 _newPosleftDoor = new Vector3(-3.5f, 2f, 14.5f);
        Vector3 _newPosRightDoor = new Vector3(3.5f, 2f, 14.5f);

        protected void Start()
        {
            _device.onOpenDoorEvent += Open;

            _audioSource = GetComponent<AudioSource>();
        }

        private void Open()
        {
            //StartCoroutine(StartingMechanism());
            _audioSource.PlayOneShot(_audioClipOpenDoor);

            _leftDoor.transform.localPosition = _newPosleftDoor;
            _RightDoor.transform.localPosition = _newPosRightDoor;
        }

        //private IEnumerator StartingMechanism()
        //{
        //    while ()
        //    {

        //    }
        //}
    }
}
