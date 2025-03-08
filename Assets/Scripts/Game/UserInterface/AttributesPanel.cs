using System.Collections;
using System.Collections.Generic;
using Game.CharacterUtility;
using Game.EquipmentItems;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.UserInterface
{
    public class AttributesPanel : Control
    {
        CharacterRightPanel m_CharacterRightPanel;

        public CharacterRightPanel characterRightPanel
        {
            get => m_CharacterRightPanel;
        }

        VisualElement m_AttributesContainer;

        Equipment m_Equipment;

        public Equipment equipment
        {
            get => m_Equipment;
            set => SetEquipment(value);
        }

        public AttributesPanel(CharacterRightPanel characterRightPanel, VisualElement rootElement) : base(rootElement)
        {
            m_CharacterRightPanel = characterRightPanel;
            m_AttributesContainer = rootElement.Q("AttributesContainer");
            equipment = null;
        }

        public void SetEquipment(Equipment equipment)
        {
            if (m_Equipment != null)
            {
                m_Equipment.onChanged -= OnEquipmentChanged;
            }

            m_Equipment = equipment;
            m_AttributesContainer.Clear();
            if (equipment != null)
            {
                m_Equipment.onChanged += OnEquipmentChanged;
                RefreshAttributes();
            }
        }

        void RefreshAttributes()
        {
            m_AttributesContainer.Clear();
            var attributes = Item.AvailableAttributes;
            foreach (var item in equipment.items)
            {
                // Item can be null when equipment slot is empty (just like in case of inventory).
                if (item == null)
                {
                    continue;
                }

                for (int i = 0; i < item.attributes.Count; i++)
                {
                    attributes[i].value += item.attributes[i].value;
                }
            }

            foreach (var attribute in attributes)
            {
                if (attribute.isEmpty)
                {
                    continue;
                }

                var attributeControl = new AttributeControl()
                {
                    attribute = attribute,
                    valueChangeVisible = false,
                };

                m_AttributesContainer.Add(attributeControl);
            }
        }

        void OnEquipmentChanged(int index)
        {
            RefreshAttributes();
        }
    }
}
