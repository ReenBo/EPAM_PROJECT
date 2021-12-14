using ET.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ET
{
    public class CameraFollowPlayer : MonoBehaviour, IMainCamera
    {
        private IPlayer _player;

        private Transform _playerTransform;
        private Vector3 _distance;

        [Header("Camera parameters")]
        [Range(0, 10)]
        [SerializeField] private float _speed = 1;
        [SerializeField] private float _power = 0.2f;
        [SerializeField] private float _duration = 0.2f;
        [SerializeField] private float _slowDownAmount = 1f;

        private float _initialDuration = 0.2f;

        protected void FixedUpdate()
        {
            if (_playerTransform)
            {
                FollowPlayer();
            }
        }

        public void GetPlayerPosition(IPlayer player, Transform target)
        {
            _playerTransform = target;
            _distance = transform.position - _playerTransform.position;

            _player = player;

            _player.PlayerCombat.OnShake += Shake;
        }

        private void FollowPlayer()
        {
            transform.position = Vector3.Lerp(
                transform.position,
                _playerTransform.position + _distance,
                _speed * Time.deltaTime);
        }

        private void Shake()
        {
            var currentPos = transform.localPosition;

            if (_duration > 0f)
            {
                transform.localPosition = transform.localPosition + Random.insideUnitSphere * _power;
                _duration -= Time.deltaTime * _slowDownAmount;
            }
            else
            {
                _duration = _initialDuration;
                transform.localPosition = currentPos;
            }
        }

        protected void OnDestroy()
        {
            _player.PlayerCombat.OnShake -= Shake;
        }
    }
}
