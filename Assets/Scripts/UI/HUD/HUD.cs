using ET.Interface;
using ET.Player;
using ET.Player.UI.ExperienceView;
using ET.Player.UI.StatsView;
using ET.UI.SkillsView;
using ET.UI.Weapon;
using UnityEngine;

namespace ET.UI
{
    public class HUD : MonoBehaviour, IHUD
    {
        private IPlayer _player;

        [SerializeField] private PlayerStatsView _playerStatsView;
        [SerializeField] private PlayerExperienceView _playerExperienceView;
        [SerializeField] private PlayerSkillsView _playerSkillsView;
        [SerializeField] private WeaponView _weaponView;

        protected void Awake()
        {
            _player = GameManager.Instance.GetPlayer();
        }

        public PlayerStatsView PlayerStatsView { get => _playerStatsView; set => _playerStatsView = value; }
        public PlayerExperienceView PlayerExperienceView { get => _playerExperienceView; set => _playerExperienceView = value; }
        public PlayerSkillsView PlayerSkillsView { get => _playerSkillsView; set => _playerSkillsView = value; }
        public WeaponView WeaponView { get => _weaponView; set => _weaponView = value; }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void ReceiveStatusOfSubscribersHandler(bool status)
        {
            if (status)
            {
                GameManager.Instance.PlayerCombatController.onPlayerStatsViewChange +=
                    PlayerStatsView.SetAmmoCountViem;

                GameManager.Instance.PlayerController.onArmorViewChange += PlayerStatsView.SetArmorView;
                GameManager.Instance.PlayerController.onHealthViewChange += PlayerStatsView.SetHealthView;

                GameManager.Instance.levelSystem.onExperiencePlayerChange += 
                    PlayerExperienceView.SetExperience;

                GameManager.Instance.WeaponsController.onAmmoCountViemChange +=
                    PlayerStatsView.SetAmmoCountViem;

                GameManager.Instance.PlayerCombatController.onWeaponViewChange +=
                    WeaponView.DisplayWeapon;

                GameManager.Instance.RecoverySkill.onDisplaySkill += PlayerSkillsView.DisplaySkills;

                GameManager.Instance.RecoverySkill.onHealthViewChange += PlayerStatsView.SetHealthView;

            }
            else
            {
                GameManager.Instance.PlayerCombatController.onPlayerStatsViewChange -=
                    PlayerStatsView.SetAmmoCountViem;

                GameManager.Instance.PlayerController.onArmorViewChange -= PlayerStatsView.SetArmorView;
                GameManager.Instance.PlayerController.onHealthViewChange -= PlayerStatsView.SetHealthView;

                GameManager.Instance.levelSystem.onExperiencePlayerChange -=
                    PlayerExperienceView.SetExperience;

                GameManager.Instance.WeaponsController.onAmmoCountViemChange -=
                    PlayerStatsView.SetAmmoCountViem;

                GameManager.Instance.PlayerCombatController.onWeaponViewChange -=
                    WeaponView.DisplayWeapon;

                GameManager.Instance.RecoverySkill.onDisplaySkill -= PlayerSkillsView.DisplaySkills;

                GameManager.Instance.RecoverySkill.onHealthViewChange -= PlayerStatsView.SetHealthView;
            }
        }
    }
}
