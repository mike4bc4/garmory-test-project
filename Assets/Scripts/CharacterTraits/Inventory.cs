using System.Collections;
using System.Collections.Generic;
using EquipmentItems;
using UnityEngine;

namespace CharacterTraits
{
    public class Inventory
    {
        const int k_InitialSize = 100;

        Character m_Character;

        public Character character
        {
            get => m_Character;
        }

        int m_Size;

        public int size
        {
            get => m_Size;
            set => m_Size = value;
        }

        List<Item> m_Items;

        public IReadOnlyList<Item> items
        {
            get => m_Items.AsReadOnly();
        }

        public Inventory(Character character)
        {
            m_Character = character;
            m_Items = new List<Item>();
            m_Size = k_InitialSize;
        }
    }
}
