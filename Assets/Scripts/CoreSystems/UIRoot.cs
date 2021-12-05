using ET.Interface;
using ET.Player;
using ET.UI;
using ET.UI.SkillsView;
using ET.UI.WindowTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.UI
{
    public class UIRoot : MonoBehaviour, IUIRoot
    {
        #region Singleton
        //private static UIRoot _instance = null;

        //public static UIRoot Instance
        //{
        //    get
        //    {
        //        if (_instance == null)
        //        {
        //            Debug.LogError("UIRoot is NULL");
        //        }

        //        return _instance;
        //    }
        //}
        #endregion

        private bool _isVisible = false;

        private IPlayer _player = null;
        private IPopups _popups = null;
        private IHUD _hUD = null;

        protected void Awake()
        {
            _popups = GameManager.Instance.GetPopups();
            _popups.PopupsTransform.SetParent(transform);

            _hUD = GameManager.Instance.GetHUD();
            _hUD.HUDTransform.SetParent(transform);
        }

        protected void Start()
        {
            _hUD.Show();
        }

        public void Init(IPlayer player, ILevelSystem levelSystem)
        {
            _player = player;

            _hUD.Init(_player, levelSystem);

            _player.onOpenWindow += OpenWindow;
            _player.onCloseWindow += CloseWindow;
        }

        protected void OnDestroy()
        {
            _hUD.Hide();

            _player.onOpenWindow -= OpenWindow;
            _player.onCloseWindow -= CloseWindow;
        }

        public void OpenWindow(WindowType window)
        {
            if (!_isVisible)
            {
                _popups.UIObjects[window].Show();
                _isVisible = true;
            }
        }

        public void CloseWindow(WindowType window)
        {
            if (_isVisible)
            {
                _popups.UIObjects[window].Hide();
                _isVisible = false;
            }
        }

        public void CloseAllWindow()
        {
            if (_isVisible)
            {
                foreach (var pair in _popups.UIObjects.Values)
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
    }
}
