using System;
using UnityEngine;

namespace ET.Interface
{
    public interface IMainMenu : IUIScreenable
    {
        Transform MainMenuTrans { get; }

        void Init(IPreloader preloader);
    }
}

