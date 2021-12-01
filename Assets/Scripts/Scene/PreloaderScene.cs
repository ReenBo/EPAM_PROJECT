using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using ET.Core.LevelInfo;
using ET.UI;
using ET.UI.WindowTypes;
using ET.Interface;
using ET.Core;
using ET.Enums.EComponents;
using ET.Enums.Scenes;
using UnityEngine.EventSystems;
using ET.Enums.Views;

namespace ET
{
    public class PreloaderScene : MonoBehaviour, IPreloader
    {
        private ILoadingScreen loadingScreen = null;
        public IResoursManager _resoursManager = null;
        public IScenesManager scenesManager = null;
        public IMainMenu mainMenu = null;

        private AsyncOperation _loading = null;

        protected void Awake()
        {
            _resoursManager = new ResoursesManager();
            scenesManager = new ScenesManager(this);

            gameObject.AddComponent<EventSystem>();
            gameObject.AddComponent<StandaloneInputModule>();

            loadingScreen = _resoursManager.CreateObjectInstance<ILoadingScreen, EComponents>(EComponents.LoadingScreen);
            mainMenu = _resoursManager.CreateObjectInstance<IMainMenu, EView>(EView.MainMenu);

            DontDestroyOnLoad(gameObject);
        }

        protected void Start()
        {
            SceneManager.LoadSceneAsync(SceneIndex._MainMenu.ToString(), LoadSceneMode.Additive);
            mainMenu.Init(this);
            mainMenu.Show();
        }

        public IResoursManager GetResourseManager()
        {
            if (_resoursManager is null)
            {
                _resoursManager = new ResoursesManager();
            }

            return _resoursManager;
        }

        public IScenesManager GetScenesManager()
        {
            if (scenesManager is null)
            {
                scenesManager = new ScenesManager(this);
            }

            return scenesManager;
        }

        public IPreloader GetPreloader()
        {
            return this;
        }

        public void UploadScene(SceneIndex scene)
        {
            StartCoroutine(AsyncLoading(scene));
        }

        private IEnumerator AsyncLoading(SceneIndex scene)
        {
            loadingScreen.Show();
            
            //UIRoot.Instance.OpenWindow(WindowType.LOADING_SCREEN);

            _loading = SceneManager.LoadSceneAsync(SceneIndex._GameSession.ToString());

            _loading.allowSceneActivation = false;

            while (!_loading.isDone)
            {
                loadingScreen.LoadingLine.fillAmount += Mathf.Clamp01(1e-3f);

                if (_loading.progress >= 0.9f)
                {
                    yield return _loading.allowSceneActivation = true;
                }
            }

            //_loading = SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Additive);
            _loading = SceneManager.LoadSceneAsync(SceneIndex._TESTING.ToString(), LoadSceneMode.Additive);
            yield return _loading;


            var levelInfo = GameObject.FindGameObjectWithTag(Tags.LEVEL_INFO);
            InfoSceneObjects infoSceneObjects = levelInfo.GetComponent<InfoSceneObjects>();

            yield return null;

            yield return GameManager.Instance.InitGame(infoSceneObjects);

            loadingScreen.Hide();

            //UIRoot.Instance.CloseWindow(WindowType.LOADING_SCREEN);

            if (_loading.isDone)
            {
                GameManager.Instance.StartSession();
            }

            yield break;
        }
    }
}
