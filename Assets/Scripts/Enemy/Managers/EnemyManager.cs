using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ET.Enemy.AI;
using ET.Interface;
using Random = UnityEngine.Random;

namespace ET.Enemy
{
    public class EnemyManager : MonoBehaviour, IEnemyManager
    {
        private Transform _playerPosition;
        private readonly List<GameObject> _listEnemies = new List<GameObject>();
        private Transform[] _spawnTargets;
        private Transform _bossSpawnTarget;

        private int _amountOfEnemiesPerPoint = 12;
        private float _timer = 0f;
        private float _timeRespawn = 300f;

        [Header("Prefab Enemy")]
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private GameObject _bossPrefab;

        protected void Start()
        {
            _spawnTargets = new Transform[_amountOfEnemiesPerPoint];
        }

        private Transform[] _spawnTargetsOnLevel;

        public void Init(Transform playerPosition, Transform[] spawnTargetsOnLevel, Transform bossSpawnTarget)
        {
            _playerPosition = playerPosition;
            _spawnTargetsOnLevel = spawnTargetsOnLevel;
            _bossSpawnTarget = bossSpawnTarget;

            GenerateBoss();
            StartCoroutine(GenerateEnemies());
        }

        private IEnumerator GenerateEnemies()
        {
            yield return new WaitForEndOfFrame();

            for (int i = 0; i < _spawnTargetsOnLevel.Length; i++)
            {
                var startPoint = _spawnTargetsOnLevel[i].transform;

                for (int j = 0; j < _amountOfEnemiesPerPoint; j++)
                {
                    var x = Random.Range(-3f, 3f);
                    var z = Random.Range(-3f, 3f);
                    var result = new Vector3(x, 0, z);

                    if (startPoint.localPosition != result)
                    {
                        _spawnTargets[j] = startPoint;

                        _listEnemies.Add(CreateEnemy(_enemyPrefab, _spawnTargets[j]));

                        startPoint.localPosition = result;
                    }
                }
            }

            foreach (var enemy in _listEnemies)
            {
                enemy.GetComponent<EnemyStateController>().GetPlayerPosition(_playerPosition);
            }
        }

        private void GenerateBoss()
        {
            GameObject boss = CreateEnemy(_bossPrefab, _bossSpawnTarget);
            boss.GetComponent<EnemyStateController>().GetPlayerPosition(_playerPosition);
        }

        //private void InitializeTargetsSpawn()
        //{
        //    _childCountParentHas = transform.childCount;

        //    _spawnTarget = new Transform[_childCountParentHas];

        //    for (int i = 0; i < _childCountParentHas; i++)
        //    {
        //        _spawnTarget[i] = gameObject.GetComponentInChildren<Transform>().GetChild(i);
        //    }
        //}

        private IEnumerator Fill()
        {
            for (int i = 0; i < _spawnTargets.Length; i++)
            {
                _listEnemies.Add(CreateEnemy(_enemyPrefab, _spawnTargets[i]));
                _listEnemies[i].GetComponent<EnemyStateController>().GetPlayerPosition(_playerPosition);

                yield return new WaitForSeconds(1f);
            }
        }

        private GameObject CreateEnemy(GameObject prefab, Transform target)
        {
            var enemy = Instantiate(prefab, target.position, Quaternion.Euler(0f, Random.Range(0, 360), 0f));

            return enemy;
        }

        private void RespawnEnemies()
        {
            _timer += Time.deltaTime;

            if (_timer > _timeRespawn)
            {
                _listEnemies.Clear();

                if (_listEnemies.Count == 0)
                {
                    transform.position = _playerPosition.position;

                    StartCoroutine(Fill());
                }
                _timer = 0f;
            }
        }
    }
}
