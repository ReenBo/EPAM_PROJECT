using UnityEngine;

namespace ET.Interface
{
    public interface IHUD : IUIScreenable
    {
        Transform HUDTransform { get; }
    }
}
