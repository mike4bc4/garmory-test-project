using System.Collections;
using System.Collections.Generic;
using EquipmentItems;
using Gameplay;
using UnityEngine;

namespace Interactables
{
    public class Armory : Interactable
    {
        List<Item> m_Items;

        public IReadOnlyCollection<Item> items
        {
            get => m_Items.AsReadOnly();
        }

        bool m_GeneratingItems;
        bool m_InRange;

        void Awake()
        {
            m_Items = new List<Item>();
            state = new State("Get closer interact");
        }

        public void Clear()
        {
            m_Items.Clear();
        }

        public override void OnEnterRange()
        {
            m_InRange = true;
            if (!m_GeneratingItems)
            {
                if (m_Items.Count > 0)
                {
                    state = new State("Press E to collect items");
                }
                else
                {
                    state = new State("Press E to interact");
                }
            }
        }

        public override void OnExitRange()
        {
            m_InRange = false;
            if (!m_GeneratingItems)
            {
                if (m_Items.Count > 0)
                {
                    state = new State("Get closer to collect items");
                }
                else
                {
                    state = new State("Get closer to interact");
                }
            }
        }

        public override void OnInteract()
        {
            m_GeneratingItems = true;
            state = new State("Generating items...");
            m_Items.Clear();
            ItemsGenerator.Generate(items =>
            {
                m_Items = items;
                m_GeneratingItems = false;
                if (m_InRange)
                {
                    state = new State("Press E to collect items");
                }
                else
                {
                    state = new State("Get closer to collect items");
                }
            });
        }
    }
}
