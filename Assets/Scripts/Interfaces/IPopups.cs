using ET.Interface;
using ET.UI.WindowTypes;
using System.Collections.Generic;

namespace ET.Interface
{
    public interface IPopups
    {
        public Dictionary<WindowType, IUIScreenable> UIObjects { get; }
    }
}
