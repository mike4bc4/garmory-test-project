using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.EquipmentItems;
using UnityEngine;

namespace Game.CharacterUtility
{
    public class Inventory
    {
        public event Action<List<int>> onChanged;
        public event Action onSizeChanged;

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
            set => SetSize(value);
        }

        Item[] m_Items;

        public IReadOnlyList<Item> items
        {
            get => m_Items.ToList().AsReadOnly();
        }

        public Inventory(Character character)
        {
            m_Character = character;
            m_Items = new Item[0];
            size = k_InitialSize;
        }

        public void SetSize(int size)
        {
            m_Size = size;
            Array.Resize(ref m_Items, size);
            onSizeChanged?.Invoke();
        }

        public void AddItems(List<Item> items)
        {
            var changedIndexes = new List<int>();
            foreach (var item in items)
            {
                int index = Array.IndexOf(m_Items, null);
                if (index == -1)
                {
                    break;
                }

                m_Items[index] = item;
                changedIndexes.Add(index);
            }

            if (changedIndexes.Count > 0)
            {
                onChanged?.Invoke(changedIndexes);
            }
        }

        public void RemoveItems(List<Item> items)
        {
            var changedIndexes = new List<int>();
            foreach (var item in items)
            {
                int index = Array.IndexOf(m_Items, item);
                if (index >= 0)
                {
                    m_Items[index] = null;
                    changedIndexes.Add(index);
                }
            }

            if (changedIndexes.Count > 0)
            {
                onChanged?.Invoke(changedIndexes);
            }
        }

        public void PutItem(int index, Item item)
        {
            m_Items[index] = item;
            onChanged?.Invoke(new List<int>() { index });
        }

        public void SwapItems(int indexA, int indexB)
        {
            (m_Items[indexA], m_Items[indexB]) = (m_Items[indexB], m_Items[indexA]);
            onChanged?.Invoke(new List<int>() { indexA, indexB });
        }
    }
}
