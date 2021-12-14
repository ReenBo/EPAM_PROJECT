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
        private Animator _animator;
        private BoxCollider _boxCollider;

        private PlayerCombatController _playerCombat;
        private PlayerSkillsController _playerSkills;
        private InputSystem _inputSystem;
        private SpecialToolsController _specialTools;
        private InteractionWithItems _interactionWithItems;

        [Header("Parameters Object")]
        [SerializeField] private Transform _playerPosition;
        [Range(0, 100)]
        [SerializeField] private float _maxHealth;
        [Range(0, 100)]
        [SerializeField] private float _maxArmor;

        public event Action<float, int> onArmorViewChange;
        public event Action<float, int> onHealthViewChange;
        public event Action<WindowType> onOpenWindow;
        public event Action<WindowType> onCloseWindow;

        private List<GameObject> _keyCards = new List<GameObject>();

        private float _currentHealth = 0;
        private float _currentArmor = 0;

        private bool _isDead = false;
        private bool _isVisible = false;

        public float CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
        public float CurrentArmor { get => _currentArmor; set => _currentArmor = value; }

        public Transform PlayerPosition { get => _playerPosition; }

        public PlayerCombatController PlayerCombat { get => _playerCombat; }
        public PlayerSkillsController PlayerSkills { get => _playerSkills; }
        public InputSystem InputSystem { get => _inputSystem; }
        public List<GameObject> KeyCards { get => _keyCards; }

        protected void Awake()
        {
            _currentHealth = _maxHealth;
            _currentArmor = _maxArmor;

            _animator = GetComponent<Animator>();
            _boxCollider = GetComponent<BoxCollider>();
            _inputSystem = GetComponent<InputSystem>();
            _playerCombat = GetComponent<PlayerCombatController>();
            _playerSkills = GetComponent<PlayerSkillsController>();
        }

        protected void LateUpdate()
        {
            SwitchWindow();
        }

        public Transform GetPosition()
        {
            return transform;
        }

        public void SetPosition(Transform target)
        {
            transform.position = target.position;
        }

        protected void OnTriggerEnter(Collider collider)
        {
            var key = collider.GetComponent<IKeyCard>();

            if(key != null)
            {
                _keyCards.Add(collider.gameObject);
            }
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

        private void SwitchWindow()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!_isVisible)
                {
                    onOpenWindow(WindowType.PAUSE_MENU);
                    _isVisible = true;
                }
                else
                {
                    onCloseWindow(WindowType.PAUSE_MENU);
                    _isVisible = false;
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

                onOpenWindow.Invoke(WindowType.GAME_OVER);
            }
        }
    }
}
