using ET.Interface;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ET.UI
{
    public class LoadingViewController : MonoBehaviour, ILoadingScreen
    {
        [SerializeField] private Image _loadingLine;
        private Transform _loadingScreenransform;

        public Image LoadingLine { get => _loadingLine; set => _loadingLine = value; }
        public Transform LoadingScreenTransform { get => _loadingScreenransform; }

        protected void Awake()
        {
            _loadingScreenransform = transform;
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
