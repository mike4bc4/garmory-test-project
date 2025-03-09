using System.Collections;
using System.Collections.Generic;
using InputUtility;
using SchedulerUtility;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Game.UserInterface
{
    public class WelcomePanel : Control
    {
        MainPanel m_MainPanel;

        public MainPanel mainPanel
        {
            get => m_MainPanel;
        }

        Button m_ContinueButton;

        public WelcomePanel(MainPanel mainPanel)
        {
            m_MainPanel = mainPanel;
            var template = UserInterfaceManager.Instance.welcomePanel.Instantiate();
            rootElement = template.hierarchy[0];

            m_ContinueButton = rootElement.Q<Button>("ContinueButton");
            m_ContinueButton.clicked += () =>
            {
                InputManager.InputActions.gameplay.Enable();
                rootElement.style.display = DisplayStyle.None;
            };
        }
    }
}
