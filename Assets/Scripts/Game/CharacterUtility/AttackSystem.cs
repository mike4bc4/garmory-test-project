using System;
using System.Collections;
using System.Collections.Generic;
using Game.Enemies;
using Game.VisualEffects;
using InputUtility;
using SchedulerUtility;
using UnityEngine;

namespace Game.CharacterUtility
{
    public class AttackSystem
    {
        [Serializable]
        public class Settings
        {
            [SerializeField] Slash m_SlashEffect;

            public Slash slashEffect
            {
                get => m_SlashEffect;
            }

            [SerializeField] float m_AttackTime;

            public float attackTime
            {
                get => m_AttackTime;
            }

            [SerializeField] float m_AttackRange;

            public float attackRange
            {
                get => m_AttackRange;
            }

            [SerializeField] float m_BaseDamage;

            public float baseDamage
            {
                get => m_BaseDamage;
            }
        }

        Character m_Character;

        public Character character
        {
            get => m_Character;
        }

        Settings m_Settings;

        public Settings settings
        {
            get => m_Settings;
        }

        InputActions m_InputActions;

        bool m_CanAttack;

        public AttackSystem(Character character, Settings settings)
        {
            m_Character = character;
            m_Settings = settings;

            m_CanAttack = true;

            m_InputActions = new InputActions();
            m_InputActions.gameplay.attack.Enable();
            m_InputActions.gameplay.attack.performed += ctx =>
            {
                if (m_CanAttack)
                {
                    m_CanAttack = false;
                    Scheduler.Wait(settings.attackTime, () => m_CanAttack = true);
                    settings.slashEffect.Play();

                    foreach (var enemy in GetAttackedEnemies())
                    {
                        enemy.TakeDamage(settings.baseDamage);
                    }
                }
            };
        }

        List<Enemy> GetAttackedEnemies()
        {
            var enemies = new List<Enemy>();
            foreach (var enemy in EnemyManager.Instance.enemies)
            {
                var distance = Vector3.Distance(character.transform.position, enemy.transform.position);
                var dir = (enemy.transform.position - character.transform.position).normalized;
                var dot = Vector3.Dot(character.transform.forward, dir);
                if (distance < settings.attackRange && dot > 0.5f)
                {
                    enemies.Add(enemy);
                }
            }

            return enemies;
        }

    }
}
