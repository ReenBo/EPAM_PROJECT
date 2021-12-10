using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ET.Interface;

namespace ET.UI 
{
    public class CreateMainMenu : MonoBehaviour
    {
        private IPreloader _preloader;
        private IScenesManager _scenesManager; 
        protected IMainMenu _mainMenu;

        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject _menu;

        //protected void Awake()
        //{
        //    Instantiate(_camera);
        //    Instantiate(_menu);

        //    _mainMenu = _menu.GetComponent<IMainMenu>();
        //    Debug.Log(_mainMenu);

        //    _mainMenu.Show();

        //    var preloader = GameObject.FindGameObjectWithTag("PreLoader");
        //    _preloader = preloader.GetComponent<IPreloader>();

        //    _scenesManager = _preloader.GetScenesManager();

        //}

        //protected void Start()
        //{
        //    //_mainMenu.Show();

        //    _mainMenu.OnPlayGameClicked += _scenesManager.StartGame;
        //    _mainMenu.OnLoadGameClicked += _scenesManager.LoadGame;
        //    _mainMenu.OnSettingsClicked += _scenesManager.SettingsGame;
        //    _mainMenu.OnPlayGameClicked += _scenesManager.ExitGame;
        //}

        //protected void OnDestroy()
        //{
        //    _mainMenu.Hide();

        //    _mainMenu.OnPlayGameClicked -= _scenesManager.StartGame;
        //    _mainMenu.OnLoadGameClicked -= _scenesManager.LoadGame;
        //    _mainMenu.OnSettingsClicked -= _scenesManager.SettingsGame;
        //    _mainMenu.OnPlayGameClicked -= _scenesManager.ExitGame;
        //}
    }
}
