using ET.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public enum TypeKeyCard
    {
        CYAN_KEY
    }

    public class KeyCardMovement : MonoBehaviour, IKeyCard
    {
        private GameObject _keyCardGameObject;
        private TypeKeyCard _typeKeyCard = TypeKeyCard.CYAN_KEY;
        private AudioSource _audioSource;

        [SerializeField] private AudioClip _audioCard;

        private Rigidbody _rigidbody;

        float newY;

        public TypeKeyCard TypeKeyCard { get => _typeKeyCard; }
        public GameObject KeyCardGameObject { get => _keyCardGameObject; }

        protected void Awake()
        {
            _keyCardGameObject = gameObject;
            _rigidbody = GetComponent<Rigidbody>();
            _audioSource = GetComponent<AudioSource>();
        }

        protected void FixedUpdate()
        {
            RotateCard();
        }

        protected void RotateCard()
        {
            newY += 1f;
            Quaternion newRotation = Quaternion.Euler(0f, newY, 0f);
            _rigidbody.rotation = newRotation;
        }

        protected void OnTriggerEnter(Collider collider)
        {
            var player = collider.GetComponent<IPlayer>();

            if (player != null)
            {
                _audioSource.PlayOneShot(_audioCard);
                gameObject.SetActive(false);
            }
        }
    }
}
