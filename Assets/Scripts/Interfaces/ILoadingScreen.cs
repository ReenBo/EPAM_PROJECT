using UnityEngine;
using UnityEngine.UI;

namespace ET.Interface
{
    public interface ILoadingScreen : IUIScreenable
    {
        public Image LoadingLine { get; set; }
        Transform LoadingScreenTransform { get; }
    }
}

