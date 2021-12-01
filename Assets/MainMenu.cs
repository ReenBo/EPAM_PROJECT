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

        [SerializeField] private Button _playGame;
        [SerializeField] private Button _load;
        [SerializeField] private Button _settings;
        [SerializeField] private Button _exitGame;

        public event Action<Button> OnPlayGameClicked;
        public event Action<Button> OnLoadGameClicked;
        public event Action<Button> OnSettingsClicked;
        public event Action<Button> OnExitGameClicked;

        protected void Awake()
        {
            _playGame.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            _preloader.UploadScene(SceneIndex._Level_1);
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
