using ET.Interface;
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

        private GameObject _flashCube;

        private float multiplier = 2f;

        public void ExecuteCommand()
        {
            _flashCube = Instantiate(_prefabFlashCube, _spawnTarget.position, Quaternion.identity);

            Rigidbody rigidbody = _flashCube.GetComponent<Rigidbody>();

            Vector3 dir = _target.position - _spawnTarget.position;

            rigidbody.AddForce(new Vector3(dir.x * multiplier, dir.y * 3f, dir.z * multiplier), ForceMode.Impulse);
        }
    }
}
