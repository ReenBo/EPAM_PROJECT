using ET.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Player.Skills
{
    public class PlayerSkillsController : MonoBehaviour
    {
        private PlayerController _playerController = null;

        [SerializeField] private RecoverySkill _recoverySkill;

        private readonly KeyCode[] _keyCodes = new KeyCode[]
        {
            KeyCode.None,
            KeyCode.Q,
            KeyCode.E,
        };

        private float _healthTimeCounter = 120f;

        private float _maxHealth = 100f;
        private bool _healthIsRestored = true;


        private bool _resetIsAvailable = true;

        public RecoverySkill RecoverySkill { get => _recoverySkill; }

        protected void Start()
        {
            _playerController = GetComponent<PlayerController>();
        }

        //private void Update()
        //{
        //    ApplySkills();
        //}

        private void ApplySkills()
        {
            for (int i = 0; i < _keyCodes.Length; i++)
            {
                if (Input.GetKey(_keyCodes[i]) && _resetIsAvailable)
                {
                    ActivateSkill(i);
                    StartCoroutine(EnableResetTimer(_healthTimeCounter));
                }
            }
        }

        private void ActivateSkill(int indexSkills)
        {
            if(indexSkills == 1 && _healthIsRestored)
            {
               StartCoroutine(RestoreHealth(_playerController.CurrentHealth));
               _healthIsRestored = false;
            }
        }
        private IEnumerator RestoreHealth(float amountHealth)
        {
            float cooldownTime = 10f;

            while (amountHealth < _maxHealth)
            {
                amountHealth += cooldownTime;
                _playerController.CurrentHealth = Mathf.Clamp(amountHealth, 0, _maxHealth);

                yield return new WaitForSeconds(0.5f);
            }
            _healthIsRestored = true;

            yield return null;
        }

        private IEnumerator EnableResetTimer(float time)
        {
            while (time > 1e-3)
            {
                _resetIsAvailable = false;

                time -= 1f;
                yield return new WaitForSeconds(1f);
            }

            _resetIsAvailable = true;
            yield return null;
        }
    }
}
