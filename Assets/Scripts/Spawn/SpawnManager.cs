using System.Linq;
using UnityEngine;
using Utilities;

namespace Spawn
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private SpawnInterval[] spawnIntervals;
        [SerializeField] private GameObject obstaclePrefab;

        private float _spawnInterval;
        private float _timer;
        private bool _shouldSpawn;

        public void StartSpawning()
        {
            if (_shouldSpawn)
                return;
            
            _spawnInterval = PickSpawnInterval();
            
            _shouldSpawn = true;
        }

        public void StopSpawning()
        {
            if (!_shouldSpawn)
                return;
            
            _shouldSpawn = false;
        }
        
        private void Update()
        {
            if (!_shouldSpawn)
                return;
            
            HandleSpawnTimer();
        }
        
        private void HandleSpawnTimer()
        {
            _timer += Time.deltaTime;
            
            if (_timer < _spawnInterval) 
                return;
            
            _timer = 0f;
            
            _spawnInterval = PickSpawnInterval();
            
            SpawnObstacle(transform.position, obstaclePrefab);
        }

        private float PickSpawnInterval()
        {
            var random = MathUtilities.PickOne(spawnIntervals.Select(x => x.probability).ToArray());
            var spawnInterval = spawnIntervals[random];
            
            return spawnInterval.interval;
        }

        private static void SpawnObstacle(Vector3 pos, GameObject prefab)
        {
            Instantiate(prefab, pos, Quaternion.identity);
        }
    }
}
