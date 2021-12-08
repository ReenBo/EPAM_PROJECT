using ET.Interface;
using ET.UI;
using ET.UI.WindowTypes;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Interface
{
    public interface IPopups
    {
        Transform PopupsTransform { get; }
        Dictionary<WindowType, IUIScreenable> UIObjects { get; }
        void Init(IScenesManager scenesManager);
    }
}
