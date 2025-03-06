using System.Collections;
using System.Collections.Generic;
using GameUI;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameUI
{
    public class CharacterLeftPanel : Control
    {
        CharacterPanel m_CharacterPanel;

        public CharacterPanel characterPanel
        {
            get => m_CharacterPanel;
        }

        TopBar m_TopBar;
        InventoryPanel m_InventoryPanel;

        public InventoryPanel inventoryPanel
        {
            get => m_InventoryPanel;
        }

        public List<OpenablePanel> openablePanels
        {
            get
            {
                return new List<OpenablePanel>()
                {
                    m_InventoryPanel
                };
            }
        }

        public CharacterLeftPanel(CharacterPanel characterPanel, VisualElement rootElement) : base(rootElement)
        {
            m_CharacterPanel = characterPanel;
            m_InventoryPanel = new InventoryPanel(this, rootElement.Q<VisualElement>("InventoryPanel"));
        }
    }
}
