using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ET.Weapons;
using System;
using ET.Interface;

namespace ET.Player
{
    public class PlayerCombatController : MonoBehaviour, IPlayerCombat
    {
        #region Variables
        private Animator _animator;

        private WeaponsController _currentWeapon;

        [Header("List of weapons")]
        [SerializeField] private List<GameObject> _weaponsList;

        public event Action<float, string, int> onWeaponViewChange;
        public event Action<int, int> onPlayerStatsViewChange;
        public event Action OnShake;

        private float _delayShoot = 0f;
        private float _timeDelay = 0f;

        private const string _fire1 = "Fire1";
        private const string _mouseScrollWheel = "Mouse ScrollWheel";

        private bool _isShooting = false;
        private bool _isRuning = false;

        private int[] _bulletArray = new int[4] { 0, 1, 2, 3 };
        private int _bulletIDNumber = 0;
        private int _enumNumber = 0;
        private int _numberBulletPlayerHas = 3;

        private KeyCode[] _keyCodes;
        private int _selectedWeapon = 1;
        #endregion

        public int BulletIDNumber
        {
            get => _bulletIDNumber;
            set => _bulletIDNumber = Mathf.Clamp(value, 0, _numberBulletPlayerHas);
        }
        #region Animations Hash Code
        private int _shooting = Animator.StringToHash(AnimationsTags.SHOOTING);
        private int _reloadWeapons = Animator.StringToHash(AnimationsTags.RELODING_WEAPONS);
        private int _changingWeapon = Animator.StringToHash(AnimationsTags.CHANGING_WEAPON);
        #endregion

        protected void Awake()
        {
            _animator = GetComponent<Animator>();
            _currentWeapon = GetComponentInChildren<WeaponsController>();
        }

        protected void Start()
        {
            _timeDelay = _currentWeapon.TimeDelay;

            _keyCodes = new KeyCode[]
            {
                KeyCode.None,
                KeyCode.Alpha1,
                KeyCode.Alpha2,
                KeyCode.Alpha3,
            };
        }

        protected void Update()
        {
            TakeShot();
            ReloadWeapon();
            ChangingWeapon();
            SwitchBullets();
        }

        private void TakeShot()
        {
            if (_delayShoot <= 0)
            {
                if (Input.GetButton(_fire1))
                {
                    OnShake.Invoke();

                    _isShooting = true;
                    _animator.SetTrigger(_shooting);

                    _currentWeapon.Shoot(_bulletArray[BulletIDNumber]);
                    onPlayerStatsViewChange.Invoke(
                        _currentWeapon.NumberRoundsInMagazine, _currentWeapon.AmmoCounter);

                    _delayShoot = _timeDelay;
                }
            }
            else
            {
                _isShooting = false;
                _delayShoot -= Time.deltaTime;
            }
        }

        private void ChangingWeapon()
        {
            for (int i = 0; i < _keyCodes.Length; i++)
            {
                if (Input.GetKey(_keyCodes[i]))
                {
                    PlayAnimation(i, _changingWeapon);
                    DisableWeapons();

                    var buttonNumber = i - 1;

                    EnableWeapon(buttonNumber);
                    _currentWeapon = _weaponsList[buttonNumber].GetComponent<WeaponsController>();

                    _timeDelay = _currentWeapon.TimeDelay;

                    onPlayerStatsViewChange.Invoke(_currentWeapon.NumberRoundsInMagazine, _currentWeapon.AmmoCounter);
                    onWeaponViewChange.Invoke(_currentWeapon.TimeDelay, _currentWeapon.WeaponType.ToString(), buttonNumber);
                }
            }
        }

        private void DisableWeapons()
        {
            foreach (var weapon in _weaponsList)
            {
                weapon.SetActive(false);
            }
        }

        private void EnableWeapon(int weaponIndex)
        {
            _weaponsList[weaponIndex].SetActive(true);
        }

        private void PlayAnimation(int weaponNumber, int animation)
        {
            if (_selectedWeapon != weaponNumber)
            {
                _animator.SetTrigger(animation);
            }

            _selectedWeapon = weaponNumber;
        }

        private void SwitchBullets()
        {
            float mouseScrollNumber = Input.GetAxis(_mouseScrollWheel);
            int numberBulletsPlayerHas = 3;

            if (mouseScrollNumber > 0)
            {
                if (_enumNumber == 0)
                {
                    _enumNumber = numberBulletsPlayerHas;
                }
                else if (_enumNumber > 0)
                {
                    _enumNumber--;
                }
            }

            if (mouseScrollNumber < 0)
            {
                if (_enumNumber < numberBulletsPlayerHas)
                {
                    _enumNumber++;
                }
                else if (_enumNumber == numberBulletsPlayerHas)
                {
                    _enumNumber = 0;
                }
            }

            BulletIDNumber = _enumNumber;
        }

        private void ReloadWeapon()
        {
            if (Input.GetKey(KeyCode.R))
            {
                if (_currentWeapon.AmmoCounter >= _currentWeapon.NumberRoundsInMagazine)
                {
                    return;
                }
                else
                {
                    _animator.SetTrigger(_reloadWeapons);
                    _currentWeapon.ReloadingWeapon();
                    onPlayerStatsViewChange.Invoke(_currentWeapon.NumberRoundsInMagazine, _currentWeapon.AmmoCounter);
                }
            }
        }
    }
}
