using System;
using System.Collections;
using System.Collections.Generic;
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

        NumericAttribute m_Damage;

        public NumericAttribute damage
        {
            get => m_Damage;
        }

        NumericAttribute m_HealthPoints;

        public NumericAttribute healthPoints
        {
            get => m_HealthPoints;
        }

        NumericAttribute m_Defense;

        public NumericAttribute defense
        {
            get => m_Defense;
        }

        PercentageAttribute m_LifeSteal;

        public PercentageAttribute lifeSteal
        {
            get => m_LifeSteal;
        }

        PercentageAttribute m_CriticalStrikeChance;

        public PercentageAttribute criticalStrikeChance
        {
            get => m_CriticalStrikeChance;
        }

        PercentageAttribute m_AttackSpeed;

        public PercentageAttribute attackSpeed
        {
            get => m_AttackSpeed;
        }

        PercentageAttribute m_MovementSpeed;

        public PercentageAttribute movementSpeed
        {
            get => m_MovementSpeed;
        }

        PercentageAttribute m_Luck;

        public PercentageAttribute luck
        {
            get => m_Luck;
        }

        public List<Attribute> attributes
        {
            get
            {
                var attributes = new List<Attribute>()
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

        public static List<Attribute> AvailableAttributes
        {
            get
            {
                var attributes = new List<Attribute>()
                {
                    new NumericAttribute("Damage"),
                    new NumericAttribute("Health Points"),
                    new NumericAttribute("Defense"),
                    new PercentageAttribute("Life Steal"),
                    new PercentageAttribute("Critical Strike Chance"),
                    new PercentageAttribute("Attack Speed"),
                    new PercentageAttribute("Movement Speed"),
                    new PercentageAttribute("Luck")
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
            m_Damage = new NumericAttribute("Damage") { value = itemWrapper.Damage };
            m_HealthPoints = new NumericAttribute("Health Points") { value = itemWrapper.HealthPoints };
            m_Defense = new NumericAttribute("Defense") { value = itemWrapper.Defense };
            m_LifeSteal = new PercentageAttribute("Life Steal") { value = itemWrapper.LifeSteal };
            m_CriticalStrikeChance = new PercentageAttribute("Critical Strike Chance") { value = itemWrapper.CriticalStrikeChance };
            m_AttackSpeed = new PercentageAttribute("Attack Speed") { value = itemWrapper.AttackSpeed };
            m_MovementSpeed = new PercentageAttribute("Movement Speed") { value = itemWrapper.MovementSpeed };
            m_Luck = new PercentageAttribute("Luck") { value = itemWrapper.Luck };
        }
    }
}
