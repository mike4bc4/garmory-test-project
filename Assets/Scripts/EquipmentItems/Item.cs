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

        Category m_Category;

        public Category category
        {
            get => m_Category;
        }

        Rarity m_Rarity;

        public Rarity rarity
        {
            get => m_Rarity;
        }

        NumericCharacterAttribute m_Damage;

        public NumericCharacterAttribute damage
        {
            get => m_Damage;
        }

        NumericCharacterAttribute m_HealthPoints;

        public NumericCharacterAttribute healthPoints
        {
            get => m_HealthPoints;
        }

        NumericCharacterAttribute m_Defense;

        public NumericCharacterAttribute defense
        {
            get => m_Defense;
        }

        PercentageCharacterAttribute m_LifeSteal;

        public PercentageCharacterAttribute lifeSteal
        {
            get => m_LifeSteal;
        }

        PercentageCharacterAttribute m_CriticalStrikeChance;

        public PercentageCharacterAttribute criticalStrikeChance
        {
            get => m_CriticalStrikeChance;
        }

        PercentageCharacterAttribute m_AttackSpeed;

        public PercentageCharacterAttribute attackSpeed
        {
            get => m_AttackSpeed;
        }

        PercentageCharacterAttribute m_MovementSpeed;

        public PercentageCharacterAttribute movementSpeed
        {
            get => m_MovementSpeed;
        }

        PercentageCharacterAttribute m_Luck;

        public PercentageCharacterAttribute luck
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
            if (Enum.TryParse<Category>(itemWrapper.Category, true, out var category))
            {
                m_Category = category;
            }
            else
            {
                m_Category = Category.None;
            }

            m_Rarity = (Rarity)itemWrapper.Rarity;
            m_Damage = new NumericCharacterAttribute("Damage") { value = itemWrapper.Damage };
            m_HealthPoints = new NumericCharacterAttribute("Health Points") { value = itemWrapper.HealthPoints };
            m_Defense = new NumericCharacterAttribute("Defense") { value = itemWrapper.Defense };
            m_LifeSteal = new PercentageCharacterAttribute("Life Steal") { value = itemWrapper.LifeSteal };
            m_CriticalStrikeChance = new PercentageCharacterAttribute("Critical Strike Chance") { value = itemWrapper.CriticalStrikeChance };
            m_AttackSpeed = new PercentageCharacterAttribute("Attack Speed") { value = itemWrapper.AttackSpeed };
            m_MovementSpeed = new PercentageCharacterAttribute("Movement Speed") { value = itemWrapper.MovementSpeed };
            m_Luck = new PercentageCharacterAttribute("Luck") { value = itemWrapper.Luck };
        }
    }
}
