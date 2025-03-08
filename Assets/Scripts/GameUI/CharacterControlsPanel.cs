using System.Collections;
using System.Collections.Generic;
using GameUI.CustomControls;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameUI
{
    public class CharacterControlsPanel : Control
    {
        CharacterPanel m_CharacterPanel;

        public CharacterPanel characterPanel
        {
            get => m_CharacterPanel;
        }

        Button m_InventoryButton;

        public CharacterControlsPanel(CharacterPanel characterPanel, VisualElement rootElement) : base(rootElement)
        {
            m_CharacterPanel = characterPanel;
            m_InventoryButton = rootElement.Q<Button>("InventoryButton");
            m_InventoryButton.clicked += OnInventoryButtonClicked;
        }

        void OnInventoryButtonClicked()
        {
            var inventoryPanel = characterPanel.leftPanel.inventoryPanel;
            if (inventoryPanel.isOpen)
            {
                inventoryPanel.Close();
            }
            else
            {
                foreach (var panel in characterPanel.leftPanel.openablePanels)
                {
                    panel.Close();
                }

                inventoryPanel.Open();
            }
        }
    }
}
