using ET.Interface.UI;
using ET.UI.GameOver;
using ET.UI.LoadingView;
using ET.UI.PauseMenu;
using ET.UI.WindowTypes;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace ET.UI
{
    public class PopupList : MonoBehaviour
    {
        [SerializeField] private PauseMenuWindow _pauseMenuWindow;
        [SerializeField] private GameOverWindow _gameOverWindow;
        [SerializeField] private LoadingViewController _loadingView;

        public PauseMenuWindow PauseMenuWindow { get => _pauseMenuWindow; }
        public GameOverWindow GameOverWindow { get => _gameOverWindow; }
        private Dictionary<WindowType, GameObject> _uiObjects { get; set; }


        protected void Awake()
        {
            _uiObjects = new Dictionary<WindowType, GameObject>
            {
                { WindowType.PAUSE_MENU, _pauseMenuWindow.gameObject },
                { WindowType.GAME_OVER, _gameOverWindow.gameObject },
                { WindowType.LOADING_SCREEN, _loadingView.gameObject }
            };
        }


        public GameObject GetWindow(WindowType windowType)
        {
            if (_uiObjects.TryGetValue(windowType, out var windowPrefab))
            {
                return windowPrefab;
            }
            
            Debug.LogError("assadsad");

            return null;
        }
    }
}
