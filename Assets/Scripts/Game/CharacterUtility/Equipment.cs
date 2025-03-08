using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.EquipmentItems;
using UnityEngine;

namespace Game.CharacterUtility
{
    public class Equipment
    {
        /// <summary>
        /// This value is actually different from size of Category enum, as it included None category.
        /// </summary>
        const int k_EquipmentCategoriesCount = 6;

        public event Action<int> onChanged;

        Character m_Character;

        public Character character
        {
            get => m_Character;
        }

        Item[] m_Items;

        public IReadOnlyList<Item> items
        {
            get
            {
                return m_Items.ToList().AsReadOnly();
            }
        }

        public Equipment(Character character)
        {
            m_Character = character;
            m_Items = new Item[k_EquipmentCategoriesCount];
        }

        public void Equip(Item item)
        {
            int index = (int)item.category;
            if (index >= 0 && index < m_Items.Length)
            {
                m_Items[index] = item;
                onChanged?.Invoke(index);
            }
        }

        public void Unequip(Category category)
        {
            int index = (int)category;
            if (index >= 0 && index < m_Items.Length)
            {
                m_Items[index] = null;
                onChanged?.Invoke(index);
            }
        }
    }
}
