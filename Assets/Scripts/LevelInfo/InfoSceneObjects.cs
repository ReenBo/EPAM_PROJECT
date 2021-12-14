using ET.Device;
using ET.Enums.Scenes;
using ET.Interface;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Core.LevelInfo
{
    public class InfoSceneObjects : MonoBehaviour, ISceneInformation
    {
        [SerializeField] private SceneIndex _levelIndex;

        [Header("References to player objects in the scene")]
        [SerializeField] private Transform _playerSpawnTarget;

        [Header("References to enemy objects in the scene")]
        [SerializeField] private Transform[] _enemySpawnTargets;
        [SerializeField] private Transform _bossSpawnTarget;

        [Header("References to static level objects in the scene")]
        [SerializeField] private GameObject _levelStructure;
        [SerializeField] private EndOfLevel _endLevel;

        public SceneIndex LevelIndex { get => _levelIndex; }

        public Transform PlayerSpawnTarget { get => _playerSpawnTarget; }
        public Transform[] EnemySpawnTargets { get => _enemySpawnTargets; }
        public Transform BossSpawnTarget { get => _bossSpawnTarget; }
        public EndOfLevel EndLevel { get => _endLevel; }
    }
}
