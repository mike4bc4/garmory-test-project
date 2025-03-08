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

        PinPanel m_PinPanel;

        public PinPanel pinPanel
        {
            get => m_PinPanel;
        }

        public MainPanel(UserInterfaceManager uiManager, VisualElement rootElement) : base(rootElement)
        {
            m_UIManager = uiManager;
            m_CharacterPanelContainer = rootElement.Q<VisualElement>("CharacterPanelContainer");
            m_CharacterPanel = new CharacterPanel(this, m_CharacterPanelContainer.Q<VisualElement>("CharacterPanel"));

            m_CharacterButton = rootElement.Q<Button>("CharacterButton");
            m_CharacterButton.clicked += OnCharacterButtonClicked;

            m_DraggablePanel = new DraggablePanel(this, rootElement.Q("DraggablePanel"));
            m_PinPanel = new PinPanel(this, rootElement.Q("PinPanel"));
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
