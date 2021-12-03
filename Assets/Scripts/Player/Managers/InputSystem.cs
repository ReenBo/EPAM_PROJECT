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
        private PlayerSkillsController _skillsController;
        private SpecialToolsController _specialToolsController;
        private InteractionWithItems _interactionWithItems;

        private Dictionary<KeyCode, ICommand> _commands;

        //public event Action<WindowType> onOpenWindow;
        //public event Action<WindowType> onCloseWindow;

        protected void Awake()
        {
            _skillsController = GetComponent<PlayerSkillsController>();
            _specialToolsController = GetComponent<SpecialToolsController>();
            _interactionWithItems = GetComponent<InteractionWithItems>();

            _commands = new Dictionary<KeyCode, ICommand>()
            {
                //{ KeyCode.Escape, _root },
                { KeyCode.Q, _skillsController.RecoverySkill },
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
            foreach (var command in _commands)
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
