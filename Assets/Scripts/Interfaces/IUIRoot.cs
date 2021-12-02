using ET.UI.WindowTypes;

namespace ET.Interface
{
    public interface IUIRoot
    {
        void OpenWindow(WindowType window);
        void CloseWindow(WindowType window);
        void CloseAllWindow();
    }
}
