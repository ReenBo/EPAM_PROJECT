using ET.Interface;
using ET.Player;
using ET.Player.InputSystem;
using ET.UI;
using ET.UI.SkillsView;
using ET.UI.WindowTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.UI
{
    public class UIRoot : MonoBehaviour, IUIRoot, ICommand
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

        //[Header("References to the UI Components")]
        //[SerializeField] private Popups _popups;
        //[SerializeField] private HUD _hUD;

        private bool _isVisible = false;

        private IPopups popups = null;
        private IHUD hUD = null;

        protected void Awake()
        {
            //_instance = this;

            //DontDestroyOnLoad(gameObject);

            popups = GameManager.Instance.GetPopups();
            popups.PopupsTransform.SetParent(transform);

            hUD = GameManager.Instance.GetHUD();
            hUD.HUDTransform.SetParent(transform);

        }

        protected void Start()
        {
            hUD.Show();
        }

        protected void OnDestroy()
        {
            hUD.Hide();
        }

        public void OpenWindow(WindowType window)
        {
            if (!_isVisible)
            {
                popups.UIObjects[window].Show();
                _isVisible = true;
            }
        }

        public void CloseWindow(WindowType window)
        {
            if (_isVisible)
            {
                popups.UIObjects[window].Hide();
                _isVisible = false;
            }
        }

        public void CloseAllWindow()
        {
            if (_isVisible)
            {
                foreach (var pair in popups.UIObjects.Values)
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

        public void Ex() // TEST
        {
            if (Input.GetKey(KeyCode.Escape))
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
}
