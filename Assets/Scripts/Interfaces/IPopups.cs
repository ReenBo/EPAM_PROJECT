using ET.Interface;
using ET.UI.WindowTypes;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Interface
{
    public interface IPopups
    {
        Transform PopupsTransform { get; }
        public Dictionary<WindowType, IUIScreenable> UIObjects { get; }
    }
}
