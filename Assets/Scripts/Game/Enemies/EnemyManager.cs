using System.Collections;
using System.Collections.Generic;
using SchedulerUtility;
using UnityEngine;

namespace Game.Enemies
{
    public class EnemyManager : MonoBehaviour
    {
        static EnemyManager s_Instance;

        public static EnemyManager Instance
        {
            get => s_Instance;
        }

        public IReadOnlyList<Enemy> enemies
        {
            get => s_Instance.m_Enemies;
        }

        [SerializeField] GameObject m_EnemyPrefab;

        [SerializeField] List<Enemy> m_Enemies;

        [SerializeField] List<Transform> m_SpawnPoints;

        [SerializeField] float m_SpawnDelay;

        [SerializeField] Transform m_EnemyContainer;

        void Awake()
        {
            s_Instance = this;
        }

        void Start()
        {
            Scheduler.Wait(m_SpawnDelay, SpawnEnemy);
        }

        public void Despawn(Enemy enemy)
        {
            s_Instance.m_Enemies.Remove(enemy);
            Destroy(enemy.gameObject);
            Scheduler.Wait(m_SpawnDelay, SpawnEnemy);
        }

        void SpawnEnemy()
        {
            var spawnPoint = GetRandomSpawnPoint();
            var enemy = Instantiate(m_EnemyPrefab);
            enemy.transform.position = spawnPoint;
            enemy.transform.SetParent(m_EnemyContainer);
            m_Enemies.Add(enemy.GetComponent<Enemy>());
        }

        Vector3 GetRandomSpawnPoint()
        {
            var random = new System.Random();
            var index = random.Next(0, m_SpawnPoints.Count - 1);
            return m_SpawnPoints[index].position;
        }
    }
}
