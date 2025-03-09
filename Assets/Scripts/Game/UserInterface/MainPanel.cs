using System.Collections;
using System.Collections.Generic;
using Game.CharacterUtility;
using UnityEngine;
using UnityEngine.UIElements;


namespace Game.UserInterface
{
    public class MainPanel : Control
    {
        UserInterfaceManager m_UIManager;

        public UserInterfaceManager uiManager
        {
            get => m_UIManager;
        }

        CharacterPanel m_CharacterPanel;

        public CharacterPanel characterPanel
        {
            get => m_CharacterPanel;
        }

        VisualElement m_CharacterPanelContainer;

        Button m_CharacterButton;

        DraggablePanel m_DraggablePanel;

        public DraggablePanel draggablePanel
        {
            get => m_DraggablePanel;
        }

        FloatersPanel m_FloatersPanel;

        public FloatersPanel floatersPanel
        {
            get => m_FloatersPanel;
        }

        ObjectivePanel m_ObjectivePanel;

        public ObjectivePanel objectivePanel
        {
            get => m_ObjectivePanel;
        }

        ItemTooltipPanel m_ItemTooltipPanel;

        public ItemTooltipPanel itemTooltipPanel
        {
            get => m_ItemTooltipPanel;
        }

        public MainPanel(UserInterfaceManager uiManager, VisualElement rootElement) : base(rootElement)
        {
            m_UIManager = uiManager;
            m_CharacterPanelContainer = rootElement.Q<VisualElement>("CharacterPanelContainer");
            m_CharacterPanel = new CharacterPanel(this, m_CharacterPanelContainer.Q<VisualElement>("CharacterPanel"));

            m_CharacterButton = rootElement.Q<Button>("CharacterButton");
            m_CharacterButton.clicked += OnCharacterButtonClicked;

            m_DraggablePanel = new DraggablePanel(this, rootElement.Q("DraggablePanel"));
            m_FloatersPanel = new FloatersPanel(this, rootElement.Q("FloatersPanel"));
            m_ObjectivePanel = new ObjectivePanel(this, rootElement.Q("ObjectivePanel"));
            m_ItemTooltipPanel = new ItemTooltipPanel(this, rootElement.Q("ItemTooltipPanel"));
        }

        void OnCharacterButtonClicked()
        {
            if (characterPanel.isOpen)
            {
                characterPanel.character = null;
                characterPanel.Close();
            }
            else
            {
                characterPanel.character = CharacterManager.MainCharacter;
                characterPanel.Open();
            }
        }
    }
}
