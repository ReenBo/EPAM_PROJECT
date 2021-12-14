using ET.Enums.Scenes;
using System;

namespace ET.Interface
{
    public interface IScenesManager
    {
        event Action<SceneIndex> OnGameStarts;
        event Action OnGameProgressIsSaved;
        event Action OnGameProgressIsBeingLoaded;
        event Action OnSettingsGame;
        event Action<SceneIndex> OnGameIsBeingRestarted;
        event Action OnReturnsToMenu;

        void UpdateAfterLaunch(SceneIndex index);
        void Init(ISaveSystem saveSystem);
        void StartGame();
        void SaveGame();
        void LoadGame();
        void NextLevel();
        void SettingsGame();
        void Restart();
        void ReturnMainMenu();
        void ExitGame();

    }
}
