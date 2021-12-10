using ET.Core.LevelInfo;
using ET.Device;
using ET.Interface;
using ET.Player.Skills;
using ET.UI.WindowTypes;
using ET.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Player
{
    public class InputSystem : MonoBehaviour
    {
        private PlayerController _playerController;
        private SpecialToolsController _specialToolsController;
        private InteractionWithItems _interactionWithItems;

        private Dictionary<KeyCode, ICommand> _classCommands;

        protected void Awake()
        {
            _playerController = GetComponent<PlayerController>();
            _specialToolsController = GetComponent<SpecialToolsController>();
            _interactionWithItems = GetComponent<InteractionWithItems>();

            _classCommands = new Dictionary<KeyCode, ICommand>()
            {
                { KeyCode.Q, _playerController.PlayerSkills.RecoverySkill },
                { KeyCode.Space, _specialToolsController },
                { KeyCode.F, _interactionWithItems }
            };
        }

        protected void Update()
        {
            ActivateCommand();
        }

        private void ActivateCommand()
        {
            foreach (var command in _classCommands)
            {
                if (Input.GetKeyDown(command.Key))
                {
                    SetOnLaunch(command.Value);
                }
            }
        }

        public void SetOnLaunch(ICommand command)
        {
            command.ExecuteCommand();
        }
    }
}
