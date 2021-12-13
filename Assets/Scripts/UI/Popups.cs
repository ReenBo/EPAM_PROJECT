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
        private IScenesManager _scenesManager;

        private Transform _popupsTransform;

        [SerializeField] private PauseMenuWindow _pauseMenuWindow;
        [SerializeField] private GameOverWindow _gameOverWindow;

        public Dictionary<WindowType, IUIScreenable> UIObjects { get => _UIObjects; }
        public Transform PopupsTransform { get => _popupsTransform; }

        private Dictionary<WindowType, IUIScreenable> _UIObjects;

        protected void Awake()
        {
            _popupsTransform = transform;

            _UIObjects = new Dictionary<WindowType, IUIScreenable>
            {
                { WindowType.PAUSE_MENU, _pauseMenuWindow },
                { WindowType.GAME_OVER, _gameOverWindow },
            };
        }

        public void Init(IScenesManager scenesManager)
        {
            _scenesManager = scenesManager;

            Subscribe();
        }

        private void Subscribe()
        {
            _pauseMenuWindow.onGameHasBeenSaved += _scenesManager.SaveGame;
            _pauseMenuWindow.onGameHasRestarted += _scenesManager.Restart;
            _pauseMenuWindow.onOutOfGame += _scenesManager.ReturnMainMenu;

            _gameOverWindow.onGameHasBeenLoaded += _scenesManager.LoadGame;
            _gameOverWindow.onGameRestarted += _scenesManager.Restart;
            _gameOverWindow.onOutOfMainMenu += _scenesManager.ReturnMainMenu;
        }

        protected void OnDestroy()
        {
            _pauseMenuWindow.onGameHasBeenSaved -= _scenesManager.SaveGame;
            _pauseMenuWindow.onGameHasRestarted -= _scenesManager.Restart;
            _pauseMenuWindow.onOutOfGame -= _scenesManager.ReturnMainMenu;

            _gameOverWindow.onGameHasBeenLoaded -= _scenesManager.LoadGame;
            _gameOverWindow.onGameRestarted -= _scenesManager.Restart;
            _gameOverWindow.onOutOfMainMenu -= _scenesManager.ReturnMainMenu;
        }
    }
}
