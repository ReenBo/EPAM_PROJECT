using ET.Interface.IComand;
using ET.Interface.UI;
using ET.Player;
using ET.Player.InputSystem;
using ET.UI.HUD;
using ET.UI.LoadingView;
using ET.UI.Popups;
using ET.UI.SkillsView;
using ET.UI.WindowTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Core.UIRoot
{
    public class UIRoot : MonoBehaviour, ICommand
    {
        private static UIRoot _instance = null;

        public static UIRoot Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("UIRoot is NULL");
                }

                return _instance;
            }
        }

        private PlayerController _playerController = null;
        //private InputSystem _inputSystem = null;

        [Header("References to the UI Components")]
        [SerializeField] private Popup _popup;
        [SerializeField] private HUD _hUD;

        private bool _isVisible = false;

        public Popup Popup { get => _popup; }
        public HUD HUD { get => _hUD; }

        protected void Awake()
        {
            _instance = this;

            DontDestroyOnLoad(gameObject);
        }

        public void UpdateAfterLaunch(PlayerController playerController)
        {
            _playerController = playerController;
            //_inputSystem = playerController.GetComponent<InputSystem>();
        }

        public void OpenWindow(WindowType window)
        {
            if (!_isVisible)
            {
                _popup.UIObjects[window].Show();
                _isVisible = true;
            }
        }

        public void CloseWindow(WindowType window)
        {
            if (_isVisible)
            {
                _popup.UIObjects[window].Hide();
                _isVisible = false;
            }
        }

        public void CloseAllWindow()
        {
            if (_isVisible)
            {
                foreach (var pair in _popup.UIObjects.Values)
                {
                    pair.Hide();
                }
                _isVisible = false;
            }
        }

        public void ExecuteCommand()
        {
            if (!_isVisible)
            {
                OpenWindow(WindowType.PAUSE_MENU);
            }
            else
            {
                CloseWindow(WindowType.PAUSE_MENU);
            }
        }

        //public void ReceiveStatusOfSubscribersHandler(bool status)
        //{
        //    if (status)
        //    {
        //        _inputSystem.onOpenWindow += OpenWindow;
        //        _inputSystem.onCloseWindow += CloseWindow;
        //    }
        //    else
        //    {
        //        _inputSystem.onOpenWindow -= OpenWindow;
        //        _inputSystem.onCloseWindow -= CloseWindow;
        //    }
        //}
    }
}
