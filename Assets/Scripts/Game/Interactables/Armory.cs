using System.Collections;
using System.Collections.Generic;
using Game.CharacterUtility;
using Game.EquipmentItems;
using Game.UserInterface;
using UnityEngine;

namespace Game.Interactables
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
        FloatingPin m_Pin;

        void Start()
        {
            m_Items = new List<Item>();
            m_Pin = UserInterfaceManager.MainPanel.floatersPanel.AddPin(transform, "Get closer to interact");
            m_Pin.offset = new Vector3(0f, 1f, 0f);
        }

        public void Clear()
        {
            m_Items.Clear();
        }

        public override void OnEnterRange(Character character)
        {
            m_InRange = true;
            if (!m_GeneratingItems)
            {
                if (m_Items.Count > 0)
                {
                    m_Pin.text = "Press E to collect items";
                }
                else
                {
                    m_Pin.text = "Press E to interact";
                }
            }
        }

        public override void OnExitRange(Character character)
        {
            m_InRange = false;
            if (!m_GeneratingItems)
            {
                if (m_Items.Count > 0)
                {
                    m_Pin.text = "Get closer to collect items";
                }
                else
                {
                    m_Pin.text = "Get closer to interact";
                }
            }
        }

        public override void OnInteract(Character character)
        {
            if (m_Items.Count == 0)
            {
                m_GeneratingItems = true;
                m_Pin.text = "Generating items...";
                m_Items.Clear();
                ItemsGenerator.Generate(items =>
                {
                    m_Items = items;
                    m_GeneratingItems = false;
                    if (m_InRange)
                    {
                        m_Pin.text = "Press E to collect items";
                    }
                    else
                    {
                        m_Pin.text = "Get closer to collect items";
                    }
                });
            }
            else
            {
                character.inventory.AddItems(m_Items);
                m_Items.Clear();
                m_Pin.text = "Press E to interact";
            }
        }
    }
}
