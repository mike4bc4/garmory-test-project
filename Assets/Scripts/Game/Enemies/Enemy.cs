using System.Collections;
using System.Collections.Generic;
using Game.UserInterface;
using SchedulerUtility;
using UnityEngine;

namespace Game.Enemies
{
    public class Enemy : MonoBehaviour
    {
        float m_MaxHealth;
        float m_Health;
        FloatingHealthBar m_HealthBar;

        void Start()
        {
            m_MaxHealth = 100f;
            m_Health = 50f;

            m_HealthBar = UserInterfaceManager.MainPanel.floatersPanel.AddHealthBar(transform);
            m_HealthBar.maxHealth = m_MaxHealth;
            m_HealthBar.health = m_Health;
        }

        public void TakeDamage(float damage)
        {
            m_Health = Mathf.Max(0f, m_Health - damage);
            m_HealthBar.health = m_Health;

            if (m_Health == 0)
            {
                EnemyManager.Instance.Despawn(this);
            }
        }

        void OnDestroy()
        {
            UserInterfaceManager.MainPanel.floatersPanel.RemoveHealthBar(m_HealthBar);
        }
    }
}
