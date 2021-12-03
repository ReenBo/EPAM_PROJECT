using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ET.Core.Stats;
using System;
using ET.Player.Skills;
using ET.UI.WindowTypes;
using ET.Interface;

namespace ET.Player
{
    public class PlayerController : MonoBehaviour, IPlayer
    {
        private Animator _animator = null;
        private BoxCollider _boxCollider = null;

        private PlayerCombatController _playerCombat = null;
        private PlayerSkillsController _playerSkills = null;
        private RecoverySkill _recoverySkill = null;
        private InputSystem _inputSystem = null;
        private SpecialToolsController _specialTools = null;
        private InteractionWithItems _interactionWithItems = null;

        [Header("Parameters Object")]
        [SerializeField] private Transform _playerPosition;
        [Range(0, 100)]
        [SerializeField] private float _maxHealth;
        [Range(0, 100)]
        [SerializeField] private float _maxArmor;

        public event Action<float, int> onArmorViewChange;
        public event Action<float, int> onHealthViewChange;
        public event Action<WindowType> onPlayerDied;

        private float _currentHealth = 0;
        private float _currentArmor = 0;

        private bool _isDead = false;

        public float CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
        public float CurrentArmor { get => _currentArmor; set => _currentArmor = value; }

        public Transform PlayerPosition { get => _playerPosition; }

        public PlayerCombatController PlayerCombat { get => _playerCombat; }
        public PlayerSkillsController PlayerSkills { get => _playerSkills; }
        public RecoverySkill RecoverySkill { get => _recoverySkill; }

        protected void Awake()
        {
            _currentArmor = _maxArmor;
            _currentHealth = _maxHealth;

            _animator = GetComponent<Animator>();
            _boxCollider = GetComponent<BoxCollider>();
            _playerCombat = GetComponent<PlayerCombatController>();
            _playerSkills = GetComponent<PlayerSkillsController>();
            _recoverySkill = GetComponentInChildren<RecoverySkill>();
        }

        public Transform GetPosition()
        {
            return transform;
        }

        public void SetPosition(Transform target)
        {
            transform.position = target.position;
        }

        public void Damage(float amount)
        {
            if (gameObject != null)
            {
                if (_isDead)
                {
                    return;
                } 

                if(_currentArmor > 0f)
                {
                    _currentArmor -= amount;

                    onArmorViewChange.Invoke(amount, (int)_currentArmor);
                }
                else if(_currentArmor <= 0f)
                {
                    if (_currentHealth > 0f)
                    {
                        _currentArmor = 0f;

                        _currentHealth -= amount;

                        onHealthViewChange.Invoke(amount, (int)_currentHealth);
                    }
                    else if (_currentHealth <= 0f)
                    {
                        _currentHealth = 0f;

                        PlayerIsDying();
                    }
                }
            }
        }

        public void PlayerIsDying()
        {
            if (_isDead)
            {
                return;
            }
            else
            {
                _isDead = true;

                _boxCollider.isTrigger = true;
                gameObject.GetComponent<PlayerMovement>().enabled = false;
                _animator.SetTrigger(AnimationsTags.DEATH_TRIGGER);

                onPlayerDied.Invoke(WindowType.GAME_OVER);

                //Destroy(gameObject, 3);
            }
        }
    }
}
