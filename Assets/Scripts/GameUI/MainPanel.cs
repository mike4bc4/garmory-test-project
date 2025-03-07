using System.Collections;
using System.Collections.Generic;
using CharacterTraits;
using GameUI.CustomControls;
using UnityEngine;
using UnityEngine.UIElements;


namespace GameUI
{
    public class MainPanel : Control
    {
        UIManager m_UIManager;

        public UIManager uiManager
        {
            get => m_UIManager;
        }

        CharacterPanel m_CharacterPanel;

        public CharacterPanel characterPanel
        {
            get => m_CharacterPanel;
        }

        VisualElement m_CharacterPanelContainer;

        CustomButton m_CharacterButton;

        DraggablePanel m_DraggablePanel;

        public DraggablePanel draggablePanel
        {
            get => m_DraggablePanel;
        }

        public MainPanel(UIManager uiManager, VisualElement rootElement) : base(rootElement)
        {
            m_UIManager = uiManager;
            m_CharacterPanelContainer = rootElement.Q<VisualElement>("CharacterPanelContainer");
            m_CharacterPanel = new CharacterPanel(this, m_CharacterPanelContainer.Q<VisualElement>("CharacterPanel"));

            m_CharacterButton = rootElement.Q<CustomButton>("CharacterButton");
            m_CharacterButton.onClicked += OnCharacterButtonClicked;

            m_DraggablePanel = new DraggablePanel(this, rootElement.Q<VisualElement>("DraggedItemPanel"));
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
