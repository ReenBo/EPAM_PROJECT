using ET.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET.UI
{
    public class PauseMenuWindow : MonoBehaviour, IUIScreenable
    {
        [SerializeField] private Button _saveGame;
        [SerializeField] private Button _restart;
        [SerializeField] private Button _exitGame;

        public event Action onGameHasBeenSaved;
        public event Action onGameHasRestarted;
        public event Action onOutOfGame;

        private bool _isPaused = false;

        protected void Awake()
        {
            _saveGame.onClick.AddListener(onGameHasBeenSaved.Invoke);
            _restart.onClick.AddListener(onGameHasRestarted.Invoke);
            //_settings.onClick.AddListener(SettingsGame);
            _exitGame.onClick.AddListener(onOutOfGame.Invoke);
        }

        public void Show()
        {
            if (!_isPaused)
            {
                gameObject.SetActive(true);
                Time.timeScale = 0f;
                _isPaused = true;
            }
        }

        public void Hide()
        {
            if(_isPaused)
            {
                gameObject.SetActive(false);
                Time.timeScale = 1f;
                _isPaused = false;
            }
        }

        protected void OnDestroy()
        {
            Time.timeScale = 1f;
        }
    }
}
