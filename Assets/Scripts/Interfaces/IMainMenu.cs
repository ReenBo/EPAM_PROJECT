using System;
using UnityEngine;

namespace ET.Interface
{
    public interface IMainMenu : IUIScreenable
    {
        //event Action OnPlayGameClicked;
        //event Action OnLoadGameClicked;
        //event Action OnSettingsClicked;
        //event Action OnExitGameClicked;

        Transform MainMenuTrans { get; }

        void Init(IScenesManager scenesManager);
    }
}

