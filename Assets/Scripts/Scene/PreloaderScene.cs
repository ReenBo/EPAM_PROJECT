using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using ET.Core.LevelInfo;
using ET.UI.LoadingView;
using ET.Core.UIRoot;
using ET.UI.WindowTypes;

namespace ET.Scenes.Preloader
{
    public class PreloaderScene : MonoBehaviour
    {
        private AsyncOperation _loading = null;

        [SerializeField] private LoadingViewController _loadingLineView;

        protected void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        protected void Start()
        {
            SceneManager.LoadSceneAsync(SceneIndex._MainMenu.ToString());
        }

        public void Load(SceneIndex scene)
        {
            StartCoroutine(AsyncLoading(scene));
        }


        private IEnumerator AsyncLoading(SceneIndex scene)
        {
            UIRoot.Instance.OpenWindow(WindowType.LOADING_SCREEN);

            _loading = SceneManager.LoadSceneAsync(SceneIndex._GameSession.ToString());

            _loading.allowSceneActivation = false;

            while (!_loading.isDone)
            {
                _loadingLineView.LoadingLine.fillAmount += Mathf.Clamp01(1e-3f);

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

            UIRoot.Instance.CloseWindow(WindowType.LOADING_SCREEN);

            if (_loading.isDone)
            {
                GameManager.Instance.StartSession();
            }

            yield break;
        }
    }
}
