using ET.Interface;
using ET.Player;
using ET.Player.UI.ExperienceView;
using ET.Player.UI.StatsView;
using ET.UI.SkillsView;
using UnityEngine;

namespace ET.UI
{
    public class HUD : MonoBehaviour, IHUD
    {
        private Transform hUDTransform = null;

        private IPlayer _player;
        private IPlayerCombat _playerCombat;
        private IRecoverySkill _recoverySkill;
        private ILevelSystem _levelSystem;

        [SerializeField] private PlayerStatsView _playerStatsView;
        [SerializeField] private PlayerExperienceView _playerExperienceView;
        [SerializeField] private PlayerSkillsView _playerSkillsView;
        [SerializeField] private WeaponView _weaponView;

        public Transform HUDTransform { get => hUDTransform; }

        protected void Awake()
        {
            hUDTransform = transform;

            _player = GameManager.Instance.GetPlayer();
            _playerCombat = _player.PlayerCombat;
            _recoverySkill = _player.RecoverySkill;
            _levelSystem = GameManager.Instance.GetLevelSystem();
        }

        protected void Start()
        {
            _player.onArmorViewChange += _playerStatsView.SetArmorView;
            _player.onHealthViewChange += _playerStatsView.SetHealthView;

            _playerCombat.onPlayerStatsViewChange += _playerStatsView.SetAmmoCountViem;
            _playerCombat.onWeaponViewChange += _weaponView.DisplayWeapon;

            _levelSystem.onExperiencePlayerChange += _playerExperienceView.SetExperience;

            _recoverySkill.onDisplaySkill += _playerSkillsView.DisplaySkills;
            _recoverySkill.onHealthViewChange += _playerStatsView.SetHealthView;
        }

        protected void OnDestroy()
        {
            _player.onArmorViewChange -= _playerStatsView.SetArmorView;
            _player.onHealthViewChange -= _playerStatsView.SetHealthView;

            _playerCombat.onPlayerStatsViewChange -= _playerStatsView.SetAmmoCountViem;
            _playerCombat.onWeaponViewChange -= _weaponView.DisplayWeapon;

            _levelSystem.onExperiencePlayerChange -= _playerExperienceView.SetExperience;

            _recoverySkill.onDisplaySkill -= _playerSkillsView.DisplaySkills;
            _recoverySkill.onHealthViewChange -= _playerStatsView.SetHealthView;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
