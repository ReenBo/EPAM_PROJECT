using ET.Interface;
using ET.UI;
using ET.UI.WindowTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.UI
{
    public class Popups : MonoBehaviour, IPopups
    {
        [SerializeField] private PauseMenuWindow _pauseMenuWindow;
        [SerializeField] private GameOverWindow _gameOverWindow;
        //[SerializeField] private LoadingViewController _loadingView;

        //public PauseMenuWindow PauseMenuWindow { get => _pauseMenuWindow; }
        //public GameOverWindow GameOverWindow { get => _gameOverWindow; }

        public Dictionary<WindowType, IUIScreenable> UIObjects { get => _UIObjects; }

        private Dictionary<WindowType, IUIScreenable> _UIObjects;

        protected void Awake()
        {
            _UIObjects = new Dictionary<WindowType, IUIScreenable>
            {
                { WindowType.PAUSE_MENU, _pauseMenuWindow },
                { WindowType.GAME_OVER, _gameOverWindow },
                //{ WindowType.LOADING_SCREEN, _loadingView }
            };
        }
    }
}
