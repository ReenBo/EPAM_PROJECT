using System;

namespace ET.Interface
{
    public interface IMainMenu : IUIScreenable
    {
        //public event Action OnPlayGameClicked;
        //public event Action OnLoadGameClicked;
        //public event Action OnSettingsClicked;
        //public event Action OnExitGameClicked;

        void Init(IPreloader preloader);
    }
}

