using ET.Enums.Scenes;
using System;

namespace ET.Interface
{
    public interface IScenesManager
    {
        void UpdateAfterLaunch(SceneIndex index);
        void StartGame();
        void Restart();
        void ReturnMainMenu();
        void ExitGame();

    }
}
