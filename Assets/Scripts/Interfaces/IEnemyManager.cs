using UnityEngine;

namespace ET.Interface
{
    public interface IEnemyManager
    {
        void Init(Transform target, Transform[] transforms, Transform bossSpawnTarget);
    }
}
