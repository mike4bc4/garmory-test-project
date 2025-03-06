using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameUI
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

        public CharacterPanel(MainPanel mainPanel, VisualElement rootElement) : base(rootElement)
        {
            m_MainPanel = mainPanel;
            m_LeftPanel = new CharacterLeftPanel(this, rootElement.Q<VisualElement>("LeftPanel"));
            m_RightPanel = new CharacterRightPanel(this, rootElement.Q<VisualElement>("RightPanel"));
            m_ControlsPanel = new CharacterControlsPanel(this, rootElement.Q<VisualElement>("ControlsPanel"));
            Close();
        }
    }
}
