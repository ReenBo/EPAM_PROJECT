using ET.Enums.Scenes;
using UnityEngine;

namespace ET.Interface
{
    public interface ISceneInformation
    {
        SceneIndex LevelIndex { get; }
        Transform PlayerSpawnTarget { get; }
        Transform[] EnemySpawnTargets { get; }
        Transform BossSpawnTarget { get; }
        EndOfLevel EndLevel { get; }
    }
}
