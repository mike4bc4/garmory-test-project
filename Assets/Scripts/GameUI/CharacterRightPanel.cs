using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

namespace GameUI
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

        public CharacterRightPanel(CharacterPanel characterPanel, VisualElement rootElement) : base(rootElement)
        {
            m_CharacterPanel = characterPanel;
            m_TopBar = new TopBar(rootElement.Q<VisualElement>("TopBar"));
            m_TopBar.onCloseButtonClicked += m_CharacterPanel.Close;

            m_EquipmentPanel = new EquipmentPanel(this, rootElement.Q<VisualElement>("EquipmentPanel"));
        }
    }
}
