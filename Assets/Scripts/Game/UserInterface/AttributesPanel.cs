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

        Character m_Character;

        public Character character
        {
            get => m_Character;
            set => SetCharacter(value);
        }

        public AttributesPanel(CharacterRightPanel characterRightPanel, VisualElement rootElement) : base(rootElement)
        {
            m_CharacterRightPanel = characterRightPanel;
            m_AttributesContainer = rootElement.Q("AttributesContainer");
            character = null;
        }

        public void SetCharacter(Character character)
        {
            if (m_Character != null)
            {
                m_Character.equipment.onChanged -= OnEquipmentChanged;
            }

            m_Character = character;
            m_AttributesContainer.Clear();
            if (m_Character != null)
            {
                m_Character.equipment.onChanged += OnEquipmentChanged;
                RefreshAttributes();
            }
        }

        void RefreshAttributes()
        {
            m_AttributesContainer.Clear();
            foreach (var attribute in character.attributes)
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
