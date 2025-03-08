using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameUI
{
    public class UIManager : MonoBehaviour
    {
        UIDocument m_UIDocument;

        public UIDocument uiDocument
        {
            get => m_UIDocument;
        }

        MainPanel m_MainPanel;

        public MainPanel mainPanel
        {
            get => m_MainPanel;
        }

        void Awake()
        {
            Application.targetFrameRate = 144;
            m_UIDocument = GetComponent<UIDocument>();
            m_MainPanel = new MainPanel(this, m_UIDocument.rootVisualElement.Q<VisualElement>("MainPanel"));
        }
    }
}
