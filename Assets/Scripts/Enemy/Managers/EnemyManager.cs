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
        private Transform _playerTransform = null;
        private readonly List<GameObject> _listEnemies = new List<GameObject>();
        private Transform[] _spawnTargets;

        private int _amountOfEnemiesPerPoint = 15;
        private Vector3 _startPoint = new Vector3();
        private int _childCountParentHas = 0;
        private float _timer = 0f;
        private float _timeRespawn = 20f;

        [Header("Prefab Enemy")]
        [SerializeField] private GameObject _enemyPrefab;

        public Transform PlayerTransform { get => _playerTransform;}

        protected void Start()
        {
            _spawnTargets = new Transform[_amountOfEnemiesPerPoint];
            //if (_playerTransform)
            //{
            //    InitializeTargetsSpawn();
            //    StartCoroutine(CreateSpawnPoints());
            //}
        }

        private Transform[] _spawnTargetsOnLevel;

        public void Init(Transform playerPosition, Transform[] spawnTargetsOnLevel)
        {
            _playerTransform = playerPosition;
            _spawnTargetsOnLevel = spawnTargetsOnLevel;

            StartCoroutine(GenerateEnemies());
        }

        private IEnumerator GenerateEnemies()
        {
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

                        _listEnemies.Add(CreateEnemy(_spawnTargets[j]));
                        _listEnemies[j].GetComponent<EnemyStateController>().GetPlayerPosition(_playerTransform);

                        //yield return new WaitForSeconds(0.2f);

                        startPoint.localPosition = result;
                    }
                }

                yield return null;
            }
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
                _listEnemies.Add(CreateEnemy(_spawnTargets[i]));
                _listEnemies[i].GetComponent<EnemyStateController>().GetPlayerPosition(_playerTransform);

                yield return new WaitForSeconds(1f);
            }
        }

        private GameObject CreateEnemy(Transform target)
        {
            var enemy = Instantiate(_enemyPrefab, target.position, Quaternion.identity);

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
                    transform.position = _playerTransform.position;

                    StartCoroutine(Fill());
                }
                _timer = 0f;
            }
        }
    }
}
