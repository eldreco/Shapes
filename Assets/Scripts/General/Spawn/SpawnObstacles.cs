using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using Utils;
using static Utils.Constants;
using Random = UnityEngine.Random;

namespace General.Spawn {
    public class SpawnObstacles : MonoBehaviour {
        private static Transform _tf;

        [SerializeField] private GameObject[] obstacles;
        private int _lastSpawnedIndex = -1;
        private HashSet<ISpawnable> _spawnables;
        private Timer _spawnTimer;

        private void Start() {
            _tf = gameObject.transform;
            Assert.IsFalse(obstacles.Length == 0);

            _spawnables = obstacles.Select(v => v.GetComponent<ISpawnable>())
                .ToHashSet();
            Spawn();
        }

        private void Update() {
            _spawnTimer.ExecuteTimer();
        }

        public void OnEnable() {
            _spawnTimer = new Timer(2f);
            _spawnTimer.OnTimerFinished += Spawn;
        }

        public void OnDisable() {
            _spawnTimer.OnTimerFinished -= Spawn;
        }

        private void Spawn() {
            var spawnables = _spawnables;
            int lastSpawnedIndex = _lastSpawnedIndex;

            if (GameManager.Instance.IsLevelEnded) {
                return;
            }

            var spawnable = spawnables.Count == 1
                ? spawnables.First()
                : spawnables.OrderBy(_ => Random.Range(0, spawnables.Count))
                    .First(v => Array.IndexOf(spawnables.ToArray(), v) != lastSpawnedIndex);

            spawnable?.Spawn(_tf.position, _tf.rotation);
            _lastSpawnedIndex = Array.IndexOf(spawnables.ToArray(), spawnable);
            HandleTimer();

        }

        private void HandleTimer() {
            if (_spawnTimer.TimerMaxValue >= MIN_SPAWN_INTERVAL) {
                _spawnTimer.TimerMaxValue *= 1 / OBSTACLE_ACCELERATION;
            }

            _spawnTimer.ResetTimer();
        }
    }
}