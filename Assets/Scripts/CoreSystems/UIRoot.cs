using ET.Interface.IComand;
using ET.Interface.UI;
using ET.Player;
using ET.Player.InputSystem;
using ET.UI.HUD;
using ET.UI.LoadingView;
using ET.UI;
using ET.UI.SkillsView;
using ET.UI.WindowTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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

        [FormerlySerializedAs("_popups")]
        [Header("References to the UI Components")]
        [SerializeField] private PopupList popupList;
        [SerializeField] private HUD _hUD;

        private bool _isVisible = false;

        public PopupList PopupList { get => popupList; }
        public HUD HUD { get => _hUD; }

        protected void Awake()
        {
            _instance = this;

            DontDestroyOnLoad(gameObject);
        }

        public IUIScreenable OpenWindow<IContext>(WindowType windowType, IContext context)
        {
            if (!_isVisible)
            {
                var prefab = popupList.GetWindow(windowType);

                var window = prefab.GetComponent<IUIScreenable<IContext>>();

                if (window == null)
                {
                    Debug.LogError("asdsadsadsd");
                    return null;
                }
                
                window.Show(context);
                return window;
            }
        }

        public void CloseWindow(WindowType window)
        {
            if (_isVisible)
            {
                popupList.UIObjects[window].Hide();
                _isVisible = false;
            }
        }

        public void CloseAllWindow()
        {
            if (_isVisible)
            {
                foreach (var pair in popupList.UIObjects.Values)
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

        public void CheckingStatusGameSession(bool status)
        {
            HUD.InvolveDisplay(status);
            HUD.ReceiveStatusOfSubscribersHandler(status);
        }
    }
}
