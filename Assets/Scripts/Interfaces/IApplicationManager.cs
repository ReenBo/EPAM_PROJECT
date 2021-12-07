using System.Collections;

namespace ET.Interface
{
    public interface IApplicationManager
    {
        IResoursManager GetResourseManager();
        IPlayer GetPlayer();
        IMainCamera GetMainCamera();
        ILevelSystem GetLevelSystem();
        IUIRoot GetUIRoot();
        IHUD GetHUD();
        IPopups GetPopups();
        void StartSession();
    }
}
