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
        private Animator _animator = null;

        private WeaponsController _weaponsController = null;

        [Header("List of weapons")]
        [SerializeField] private List<GameObject> _weaponsList;
        private Dictionary<int, GameObject> _weaponDict; //!!!!!!!!!!!!

        public event Action<float, string, int> onWeaponViewChange;
        public event Action<int, int> onPlayerStatsViewChange;

        private float _delayShoot = 0f;
        private float _timeDelay = 0f;

        private const string _fire1 = "Fire1";
        private const string _mouseScrollWheel = "Mouse ScrollWheel";

        private bool _isShooting = false;
        private bool _isSwitched = false;
        //private bool _isRuning = false;

        private int[] _bulletArray = new int[4] { 0, 1, 2, 3 };
        private int _bulletIDNumber = 0;
        private int _enumNumber = 0;
        private int _numberBulletPlayerHas = 3;

        private WeapomType _weapomType;
        private string _nameWeapon = string.Empty;
        private int _amountBullets = 0;
        private int _amountAmmo = 0;

        private KeyCode[] _keyCodes;
        private int _selectedWeapon = 1;
        #endregion

        public int BulletIDNumber
        {
            get => _bulletIDNumber;
            set => _bulletIDNumber = Mathf.Clamp(value, 0, _numberBulletPlayerHas);
        }
        public WeaponsController WeaponsController { get => _weaponsController; }
        #region Animations Hash Code
        private int _shooting = Animator.StringToHash(AnimationsTags.SHOOTING);
        private int _reloadWeapons = Animator.StringToHash(AnimationsTags.RELODING_WEAPONS);
        private int _changingWeapon = Animator.StringToHash(AnimationsTags.CHANGING_WEAPON);
        #endregion

        protected void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        protected void Start()
        {
            //UpdateWeaponStats();
            //DetermineTypeOfWeapon();

            _keyCodes = new KeyCode[]
            {
                KeyCode.None,
                KeyCode.Alpha1,
                KeyCode.Alpha2,
                KeyCode.Alpha3,
            };

            _weaponDict = new Dictionary<int, GameObject>() // !!!!!!!!!!!!!!!!!!!!!!
            {
                { 1, _weaponsList[0] },
                { 2, _weaponsList[1] },
                { 3, _weaponsList[2] }
            };
        }

        protected void Update()
        {
            TakeShot();
            ReloadAmmo();
            ChangingWeapon();
            SwitchBullets();
        }

        private void TakeShot()
        {
            if (_delayShoot <= 0)
            {
                if (Input.GetButton(_fire1))
                {
                    _isShooting = true;
                    _animator.SetTrigger(_shooting);

                    _weaponsController.Shoot(_bulletArray[BulletIDNumber]);
                    onPlayerStatsViewChange.Invoke(_amountBullets, _amountAmmo);

                    _delayShoot = _timeDelay;
                }
            }
            else
            {
                _isShooting = false;
                _delayShoot -= Time.deltaTime;
            }
        }

        private void SwitchWeapon(int indexWeapon)
        {
            foreach (var item in _weaponDict)
            {
                if (item.Key.Equals(indexWeapon))
                {
                    _weaponsController = item.Value.GetComponentInChildren<WeaponsController>();
                    Debug.Log(_weaponsController);
                }
            }
        }

        private void ChangingWeapon()
        {
            for (int i = 0; i < _keyCodes.Length; i++)
            {
                if (Input.GetKey(_keyCodes[i]))
                {
                    foreach (var weapon in _weaponsList)
                    {
                        weapon.SetActive(false);
                    }

                    if(_selectedWeapon != i)
                    {
                        _animator.SetTrigger(_changingWeapon);
                    }

                    var index = i - 1;

                    SwitchWeapon(i);
                    Debug.Log("Index" + index);
                    
                    _weaponsList[i - 1].SetActive(true);

                    //DetermineTypeOfWeapon();

                    _selectedWeapon = i;

                    onWeaponViewChange.Invoke(_timeDelay, _nameWeapon, i - 1);
                    onPlayerStatsViewChange.Invoke(_amountBullets, _amountAmmo);
                }
            }
        }

        private void UpdateWeaponStats()
        {
            _weapomType = _weaponsController.WeaponType;
            _nameWeapon = _weapomType.ToString();
            _timeDelay = _weaponsController.TimeDelay;
            _amountBullets = _weaponsController.NumberRoundsInMagazine;
            _amountAmmo = _weaponsController.AmmoCounter;
            var thisAudioSource = _weaponsController.AudioSource;

            _weaponsController.AudioSource = thisAudioSource;
            _weaponsController.AmmoCounter = _amountAmmo;
        }

        //private void UpdateWeaponStats()
        //{
        //    _weaponsController = GetComponentInChildren<WeaponsController>();

        //    _weapomType = _weaponsController.WeaponType;
        //    _timeDelay = _weaponsController.TimeDelay;
        //    _amountBullets = _weaponsController.NumberRoundsInMagazine;
        //    _amountAmmo = _weaponsController.AmmoCounter;
        //    var thisAudioSource = _weaponsController.AudioSource;

        //    _weaponsController.AudioSource = thisAudioSource;
        //    _weaponsController.AmmoCounter = _amountAmmo;
        //}

        //private void DetermineTypeOfWeapon()
        //{
        //    _nameWeapon = _weapomType.ToString();
        //}

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

        private void ReloadAmmo()
        {
            if (Input.GetKey(KeyCode.R))
            {
                if (_weaponsController.AmmoCounter >= _weaponsController.NumberRoundsInMagazine)
                {
                    return;
                }
                else
                {
                    _animator.SetTrigger(_reloadWeapons);
                    _weaponsController.ReloadingWeapons();
                    onPlayerStatsViewChange.Invoke(_amountBullets, _amountAmmo);
                }
            }
        }
    }
}
