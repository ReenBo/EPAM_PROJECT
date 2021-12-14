using UnityEngine;

namespace ET.Interface
{
    public interface IMainCamera
    {
        void GetPlayerPosition(IPlayer player, Transform target);
    }
}
