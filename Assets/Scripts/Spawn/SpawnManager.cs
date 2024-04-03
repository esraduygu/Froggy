using System.Linq;
using UnityEngine;
using Utilities;

namespace Spawn
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private SpawnInterval[] spawnIntervals;
        [SerializeField] private GameObject obstaclePrefab;
        
        private float _timer;
        private float _spawnInterval;

        private void Awake()
        {
            _spawnInterval = PickSpawnInterval();
        }

        private void Update()
        {
            HandleSpawnTimer();
        }

        private void HandleSpawnTimer()
        {
            _timer += Time.deltaTime;
            
            if (_timer < _spawnInterval) 
                return;
            
            _timer = 0f;
            
            _spawnInterval = PickSpawnInterval();
            
            SpawnObstacle();
        }

        private float PickSpawnInterval()
        {
            var random = MathUtilities.PickOne(spawnIntervals.Select(x => x.probability).ToArray());
            var spawnInterval = spawnIntervals[random];
            
            return spawnInterval.interval;
        }

        private void SpawnObstacle()
        {
            var spawnPos = transform.position;
            
            Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
        }
    }
}
