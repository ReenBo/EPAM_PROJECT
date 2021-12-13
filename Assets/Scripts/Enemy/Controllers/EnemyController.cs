using ET.Enemy.AI;
using ET.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ET.Enemy
{
    public class EnemyController : MonoBehaviour, IEnemy, IDamageable
    {
        #region Variables
        private EnemyStateController _enemyState;
        private EnemyAttacksController[] _enemyAttacksController;
        private AudioSource _audioSource;

        private ILevelSystem _levelSystem;

        [Header("Parameters Object")]
        [Range(0, 100)]
        [SerializeField] private float _amountHealth = 0;
        [SerializeField] private float _amountDamage = 0;
        [SerializeField] private int _amountExperience = 0;

        [SerializeField] private GameObject _ArmR;
        [SerializeField] private GameObject _ArmL;

        [SerializeField] private AudioClip _deadAudio;
        [SerializeField] private AudioClip _hitAudio;

        [SerializeField] private ParticleSystem _bloodFX;

        public event Action<int> OnExperienceEarned;

        private bool _isDeath = false;
        #endregion

        public float AmountHealth { get => _amountHealth; set => _amountHealth = Mathf.Clamp(value, 0f, 100f); }
        public float AmountDamage { get => _amountDamage; }
        public bool isDeath { get => _isDeath; }

        protected void Awake()
        {
            _enemyState = GetComponent<EnemyStateController>();
            _enemyAttacksController = GetComponentsInChildren<EnemyAttacksController>();
            _audioSource = GetComponent<AudioSource>();

            foreach (var item in _enemyAttacksController)
            {
                item.DamageArm = _amountDamage;
            }

            _levelSystem = GameManager.Instance.GetLevelSystem();
            _levelSystem.Subscribe(this);
        }

        public void Damage(float count)
        {
            if(gameObject)
            {
                if (_isDeath) return;

                if (AmountHealth > 1e-3)
                {
                    PlayBloodEffect();

                    _audioSource.PlayOneShot(_hitAudio);

                    AmountHealth -= count;

                    int num = Random.Range(0, 10);

                    switch (num)
                    {
                        case 1:
                            StartCoroutine(_enemyState.StateStandUp());
                            break;
                        case 2:
                            StartCoroutine(_enemyState.StateHit());
                            break;
                        default:
                            break;
                    }
                }
                else if (AmountHealth < 1e-3)
                {
                    AmountHealth = 0f;
                    EnemyIsDying();
                }
            }
        }

        private void PlayBloodEffect()
        {
            var posBlood = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(1f, 1.5f), 0f);

            _bloodFX.transform.localPosition = posBlood;
            _bloodFX.Play();
        }

        public void EnemyIsDying()
        {
            OnExperienceEarned.Invoke(_amountExperience);

            _audioSource.PlayOneShot(_deadAudio);

            _ArmR.SetActive(false);
            _ArmL.SetActive(false);

            _isDeath = true;

            StopAllCoroutines();
            StartCoroutine(_enemyState.StateDeath());
        }
    }
}
