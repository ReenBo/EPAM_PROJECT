using ET.Core.UIRoot;
using ET.Interface.IComand;
using ET.Player.Skills;
using ET.UI.WindowTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Player.InputSystem
{
    public class InputSystem : MonoBehaviour
    {

        private PlayerSkillsController _skillsController;

        private Dictionary<KeyCode, ICommand> _commands;

        //public event Action<WindowType> onOpenWindow;
        //public event Action<WindowType> onCloseWindow;

        protected void Start()
        {
            _skillsController = GetComponent<PlayerSkillsController>();

            _commands = new Dictionary<KeyCode, ICommand>()
            {
                { KeyCode.Escape, UIRoot.Instance },
                { KeyCode.Q, _skillsController.RecoverySkill }
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
