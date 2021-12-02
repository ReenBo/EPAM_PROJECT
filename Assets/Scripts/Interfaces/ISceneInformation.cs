using ET.Enums.Scenes;
using UnityEngine;

namespace ET.Interface
{
    public interface ISceneInformation
    {
        SceneIndex LevelIndex { get; }
        Transform PlayerSpawnTarget { get; }
    }
}