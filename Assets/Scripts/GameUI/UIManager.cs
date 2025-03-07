using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameUI
{
    public class UIManager : MonoBehaviour
    {
        static UIManager s_Instance;

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

        public static Vector2 ScaleFactor
        {
            get
            {
                var rect = s_Instance.m_UIDocument.rootVisualElement.worldBound;
                return new Vector2(rect.width / Screen.width, rect.height / Screen.height);
            }
        }

        void Awake()
        {
            Application.targetFrameRate = 60;
            s_Instance = this;
            m_UIDocument = GetComponent<UIDocument>();
            m_MainPanel = new MainPanel(this, m_UIDocument.rootVisualElement.Q<VisualElement>("MainPanel"));
        }
    }
}
