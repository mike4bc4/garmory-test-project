using System;
using System.Collections;
using System.Collections.Generic;
using CharacterFeatures;
using GameServerUtility;
using UnityEngine;

namespace EquipmentItems
{
    public class Item
    {
        string m_Name;

        public string name
        {
            get => m_Name;
        }

        string m_Category;

        public string category
        {
            get => m_Category;
        }

        Rarity m_Rarity;

        public Rarity rarity
        {
            get => m_Rarity;
        }

        CharacterAttribute<int> m_Damage;

        public CharacterAttribute<int> damage
        {
            get => m_Damage;
        }

        CharacterAttribute<int> m_HealthPoints;

        public CharacterAttribute<int> healthPoints
        {
            get => m_HealthPoints;
        }

        CharacterAttribute<int> m_Defense;

        public CharacterAttribute<int> defense
        {
            get => m_Defense;
        }

        CharacterAttribute<float> m_LifeSteal;

        public CharacterAttribute<float> lifeSteal
        {
            get => m_LifeSteal;
        }

        CharacterAttribute<float> m_CriticalStrikeChance;

        public CharacterAttribute<float> criticalStrikeChance
        {
            get => m_CriticalStrikeChance;
        }

        CharacterAttribute<float> m_AttackSpeed;

        public CharacterAttribute<float> attackSpeed
        {
            get => m_AttackSpeed;
        }

        CharacterAttribute<float> m_MovementSpeed;

        public CharacterAttribute<float> movementSpeed
        {
            get => m_MovementSpeed;
        }

        CharacterAttribute<float> m_Luck;

        public CharacterAttribute<float> luck
        {
            get => m_Luck;
        }

        public List<CharacterAttribute> attributes
        {
            get
            {
                var attributes = new List<CharacterAttribute>()
                {
                    m_Damage,
                    m_HealthPoints,
                    m_Defense,
                    m_LifeSteal,
                    m_CriticalStrikeChance,
                    m_AttackSpeed,
                    m_MovementSpeed,
                    m_Luck,
                };

                return attributes;
            }
        }

        public Item(ServerResponseItemWrapper itemWrapper)
        {
            m_Name = itemWrapper.Name;
            m_Category = itemWrapper.Category;
            m_Rarity = (Rarity)itemWrapper.Rarity;
            m_Damage = new CharacterAttribute<int>("Damage", itemWrapper.Damage, v => v.ToString());
            m_HealthPoints = new CharacterAttribute<int>("Health Points", itemWrapper.HealthPoints, v => v.ToString());
            m_Defense = new CharacterAttribute<int>("Defense", itemWrapper.Defense, v => v.ToString());
            m_LifeSteal = new CharacterAttribute<float>("Life Steal", itemWrapper.LifeSteal, v => v.ToString());
            m_CriticalStrikeChance = new CharacterAttribute<float>("Critical Strike Chance", itemWrapper.CriticalStrikeChance, v => v.ToString());
            m_AttackSpeed = new CharacterAttribute<float>("Attack Speed", itemWrapper.AttackSpeed, v => v.ToString());
            m_MovementSpeed = new CharacterAttribute<float>("Movement Speed", itemWrapper.MovementSpeed, v => v.ToString());
            m_Luck = new CharacterAttribute<float>("Luck", itemWrapper.Luck, v => v.ToString());
        }
    }
}
