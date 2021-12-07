using ET.Interface;
using ET.Player.UI.ExperienceView;
using ET.Player.UI.StatsView;
using ET.UI.SkillsView;
using UnityEngine;

namespace ET.UI
{
    public class HUD : MonoBehaviour, IHUD
    {
        private Transform hUDTransform;

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
        }

        public void Init(IPlayer player, ILevelSystem levelSystem)
        {
            _player = player;
            _levelSystem = levelSystem;

            _playerCombat = _player.PlayerCombat;
            _recoverySkill = _player.PlayerSkills.RecoverySkill;

            Subscribe();
        }

        private void Subscribe()
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
