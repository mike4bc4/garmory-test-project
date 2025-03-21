using System;
using System.Collections;
using System.Collections.Generic;
using Game.Enemies;
using Game.UserInterface;
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

        bool m_CanAttack;

        public AttackSystem(Character character, Settings settings)
        {
            m_Character = character;
            m_Settings = settings;

            m_CanAttack = true;

            InputManager.InputActions.gameplay.attack.performed += ctx =>
            {
                if (m_CanAttack)
                {
                    m_CanAttack = false;
                    character.animationSystem.Attack(settings.attackTime);
                    Scheduler.Wait(settings.attackTime, () => m_CanAttack = true);
                    Scheduler.Wait(settings.attackTime * 0.33f, () =>
                    {
                        foreach (var enemy in GetAttackedEnemies())
                        {
                            var damage = CalculateDamage(out var isCritical);
                            var combatText = UserInterfaceManager.MainPanel.floatersPanel.AddCombatText(enemy.transform);
                            combatText.offset = new Vector3(0f, 2.5f, 0f);
                            combatText.text = string.Format(isCritical ? "{0:0.#}!" : "{0:0.#}", damage);
                            enemy.TakeDamage(damage);
                        }

                        settings.slashEffect.Play();
                    });

                }
            };
        }

        float CalculateDamage(out bool isCritical)
        {
            var damage = settings.baseDamage + character.GetAttribute(EquipmentItems.AttributeType.Damage).value;
            var critChance = character.GetAttribute(EquipmentItems.AttributeType.CriticalStrikeChance).value;
            var random = new System.Random();
            isCritical = random.Next(0, 100) < critChance;
            var multiplier = (float)random.NextDouble();
            return Mathf.Round((isCritical ? damage * 2f : damage) * (0.95f + 0.1f * multiplier));
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
