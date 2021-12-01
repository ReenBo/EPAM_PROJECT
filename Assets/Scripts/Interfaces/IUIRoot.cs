using ET.UI.WindowTypes;

public interface IUIRoot
{
    void OpenWindow(WindowType window);
    void CloseWindow(WindowType window);
    void CloseAllWindow();
}
