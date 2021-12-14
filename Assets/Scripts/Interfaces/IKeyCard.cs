using UnityEngine;

namespace ET.Interface
{
    public interface IKeyCard
    {
        TypeKeyCard TypeKeyCard { get; }
        GameObject KeyCardGameObject { get; }
    }
}
