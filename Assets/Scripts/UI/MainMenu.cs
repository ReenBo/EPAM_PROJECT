using ET.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ET.Enums.Views;
using ET.Enums.Scenes;
using UnityEngine.UI;
using System;

namespace ET.UI
{
    public class MainMenu : MonoBehaviour, IMainMenu
    {
        private IPreloader _preloader = null;

        private Transform _mainMenuTrans;

        [SerializeField] private Button _playGame;
        [SerializeField] private Button _load;
        [SerializeField] private Button _settings;
        [SerializeField] private Button _exitGame;

        public Transform MainMenuTrans { get => _mainMenuTrans; }

        //public event Action OnPlayGameClicked;
        //public event Action OnLoadGameClicked;
        //public event Action OnSettingsClicked;
        //public event Action OnExitGameClicked;

        protected void Awake()
        {
            _mainMenuTrans = transform;

            _playGame.onClick.AddListener(StartGame);
            _load.onClick.AddListener(LoadingGame);
            _settings.onClick.AddListener(SettingsGame);
            _exitGame.onClick.AddListener(ExitGame);
        }

        public void Init(IPreloader preloader)
        {
            _preloader = preloader;
        }

        private void StartGame()
        {
            _preloader.UploadScene(SceneIndex._Level_1);
        }

        private void LoadingGame()
        {

        }

        private void SettingsGame()
        {
            //
        }

        private void ExitGame()
        {
            Application.Quit();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
