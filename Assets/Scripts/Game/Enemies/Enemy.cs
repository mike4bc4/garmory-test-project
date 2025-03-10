using System.Collections;
using System.Collections.Generic;
using Game.UserInterface;
using SchedulerUtility;
using UnityEngine;

namespace Game.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] float m_MaxHealth;
        [SerializeField] float m_Health;

        [SerializeField] Animator m_Animator;

        FloatingHealthBar m_HealthBar;

        void Start()
        {
            m_HealthBar = UserInterfaceManager.MainPanel.floatersPanel.AddHealthBar(transform);
            m_HealthBar.offset = new Vector3(0f, 2.25f, 0f);
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

            m_Animator.CrossFadeInFixedTime("Base.TakeDamage", 0.25f, 0, 0f);
        }

        void OnDestroy()
        {
            UserInterfaceManager.MainPanel.floatersPanel.RemoveHealthBar(m_HealthBar);
        }
    }
}
