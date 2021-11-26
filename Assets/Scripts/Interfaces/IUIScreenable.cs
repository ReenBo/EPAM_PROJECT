using System.Threading.Tasks;

namespace ET.Interface.UI
{
    public interface IUIScreenable
    {
        Task WaitForClose();
        void Hide();
    }
    
    public interface IUIScreenable<IContext> : IUIScreenable
    {
        void Show(IContext context);
    }
}
