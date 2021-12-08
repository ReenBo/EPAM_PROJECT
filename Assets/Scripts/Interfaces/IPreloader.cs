using ET.Enums.Scenes;

namespace ET.Interface
{
    public interface IPreloader
    {
        void UploadScene(SceneIndex scene);
        void LoadMainMenu();
    }
}
