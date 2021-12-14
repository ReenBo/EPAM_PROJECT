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
        [SerializeField] private GameObject _rightDoor; ///  3.5 
        [SerializeField] private DeviceActivationController _device;
        [SerializeField] private AudioClip _audioClipOpenDoor;

        private AudioSource _audioSource;

        Vector3 _newPosleftDoor = new Vector3(-3.5f, 2f, 14.5f);
        Vector3 _newPosRightDoor = new Vector3(3.5f, 2f, 14.5f);

        protected void Start()
        {
            //_device.onOpeningDoorEvent += Open;

            _audioSource = GetComponent<AudioSource>();
        }

        public void Open()
        {
            _audioSource.PlayOneShot(_audioClipOpenDoor);

            _leftDoor.transform.localPosition = Vector3.Lerp(_leftDoor.transform.localPosition, _newPosleftDoor, 1);
            _rightDoor.transform.localPosition = Vector3.Lerp(_rightDoor.transform.localPosition, _newPosRightDoor, 1);
        }

        //private void OpeningDoors()
        //{
        //    StartCoroutine(OpeningLeftDoor());
        //}

        //private IEnumerator OpeningLeftDoor()
        //{
        //    while (_newPosleftDoor.x < _leftDoor.transform.localPosition.x)
        //    {
        //        float num = _leftDoor.transform.localPosition.x;
        //        num -= Time.fixedDeltaTime;
        //        _newPosleftDoor.x = num;
        //        Debug.Log(num);

        //        _leftDoor.transform.localPosition = _newPosleftDoor;

        //        yield return null;
        //    }
        //}
    }
}
