using System.Collections;
using System.Collections.Generic;
using Game.CharacterUtility;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.UserInterface
{
    public class CharacterPanel : OpenablePanel
    {
        MainPanel m_MainPanel;

        public MainPanel mainPanel
        {
            get => m_MainPanel;
        }

        CharacterRightPanel m_RightPanel;

        public CharacterRightPanel rightPanel
        {
            get => m_RightPanel;
        }

        CharacterLeftPanel m_LeftPanel;

        public CharacterLeftPanel leftPanel
        {
            get => m_LeftPanel;
        }

        CharacterControlsPanel m_ControlsPanel;

        public CharacterControlsPanel controlsPanel
        {
            get => m_ControlsPanel;
        }

        Character m_Character;

        public Character character
        {
            get => m_Character;
            set => SetCharacter(value);
        }

        public CharacterPanel(MainPanel mainPanel, VisualElement rootElement) : base(rootElement)
        {
            m_MainPanel = mainPanel;
            m_LeftPanel = new CharacterLeftPanel(this, rootElement.Q<VisualElement>("LeftPanel"));
            m_RightPanel = new CharacterRightPanel(this, rootElement.Q<VisualElement>("RightPanel"));
            m_ControlsPanel = new CharacterControlsPanel(this, rootElement.Q<VisualElement>("ControlsPanel"));
            Close();
            character = null;
        }

        public void SetCharacter(Character character)
        {
            m_Character = character;
            if (m_Character != null)
            {
                m_LeftPanel.inventoryPanel.inventory = character.inventory;
                m_RightPanel.attributesPanel.character = character;
                m_RightPanel.equipmentPanel.equipment = character.equipment;
            }
            else
            {
                m_LeftPanel.inventoryPanel.inventory = null;
                m_RightPanel.attributesPanel.character = null;
                m_RightPanel.equipmentPanel.equipment = null;
            }
        }
    }
}
