using ET.Interface.IComand;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Player
{
    public class SpecialToolsController : MonoBehaviour, ICommand
    {
        [SerializeField] private GameObject _prefabFlashCube;
        [SerializeField] private Transform _spawnTarget;
        [SerializeField] private Transform _target;

        private GameObject _flashCube = null;

        private float _theta = 0;

        public void ExecuteCommand()
        {
            _flashCube = Instantiate(_prefabFlashCube, 
                new Vector3(transform.position.x, 1, transform.position.z), Quaternion.identity);

            Rigidbody rigidbody = _flashCube.GetComponent<Rigidbody>();

            rigidbody.AddForce(new Vector3(0, 1, 10f), ForceMode.Impulse);



            //float radians = Time.time * Mathf.PI;
            //Debug.Log(radians);
            //_theta = Mathf.Round(radians* Mathf.Rad2Deg) % 360;
            //Debug.Log(_theta);
        }
    }
}
