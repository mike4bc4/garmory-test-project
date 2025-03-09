using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemies
{
    public class EnemyManager : MonoBehaviour
    {
        static EnemyManager s_Instance;

        public static IReadOnlyList<Enemy> Enemies
        {
            get => s_Instance.m_Enemies;
        }

        [SerializeField] List<Enemy> m_Enemies;

        void Awake()
        {
            s_Instance = this;
        }

        public static void Kill(Enemy enemy)
        {
            s_Instance.m_Enemies.Remove(enemy);
            Destroy(enemy.gameObject);
        }
    }
}
