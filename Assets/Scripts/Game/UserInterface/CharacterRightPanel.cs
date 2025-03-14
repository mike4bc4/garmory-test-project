using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

namespace Game.UserInterface
{
    public class CharacterRightPanel : Control
    {
        CharacterPanel m_CharacterPanel;

        public CharacterPanel characterPanel
        {
            get => m_CharacterPanel;
        }

        TopBar m_TopBar;

        EquipmentPanel m_EquipmentPanel;

        public EquipmentPanel equipmentPanel
        {
            get => m_EquipmentPanel;
        }

        AttributesPanel m_AttributesPanel;

        public AttributesPanel attributesPanel
        {
            get => m_AttributesPanel;
        }

        public CharacterRightPanel(CharacterPanel characterPanel, VisualElement rootElement) : base(rootElement)
        {
            m_CharacterPanel = characterPanel;
            m_TopBar = new TopBar(rootElement.Q<VisualElement>("TopBar"));
            m_TopBar.onCloseButtonClicked += m_CharacterPanel.Close;

            m_EquipmentPanel = new EquipmentPanel(this, rootElement.Q<VisualElement>("EquipmentPanel"));
            m_AttributesPanel = new AttributesPanel(this, rootElement.Q("AttributesPanel"));
        }
    }
}
