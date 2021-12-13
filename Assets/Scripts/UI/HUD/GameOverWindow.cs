using ET.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET.UI
{
    public class GameOverWindow : MonoBehaviour, IUIScreenable
    {
        [SerializeField] private Button _saveGame;
        [SerializeField] private Button _restart;
        [SerializeField] private Button _exitGame;

        private float _resetTimeGameLevel = 50f;

        public event Action onGameHasBeenLoaded;
        public event Action onGameRestarted;
        public event Action onOutOfMainMenu;

        protected void Awake()
        {
            _saveGame.onClick.AddListener(onGameHasBeenLoaded.Invoke);
            _restart.onClick.AddListener(onGameRestarted.Invoke);
            //_settings.onClick.AddListener(SettingsGame);
            _exitGame.onClick.AddListener(onOutOfMainMenu.Invoke);
        }

        public void Show()
        {
            gameObject.SetActive(true);

            StartCoroutine(ResettingTime());
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private IEnumerator ResettingTime()
        {
            float timer = _resetTimeGameLevel;

            while (true)
            {
                if (timer > 0f)
                {
                    timer -= Time.fixedDeltaTime;
                }
                else
                {
                    Hide();
                    onOutOfMainMenu.Invoke();
                }
                yield return null;
            }
        }
    }
}
