using ET.UI.WindowTypes;

namespace ET.Interface
{
    public interface IUIRoot : ICommand
    {
        void Init(IPlayer player, ILevelSystem levelSystem);
        void OpenWindow(WindowType window);
        void CloseWindow(WindowType window);
        void CloseAllWindow();
    }
}
