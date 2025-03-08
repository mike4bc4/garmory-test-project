using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace GameUI
{
    public class UIManager : MonoBehaviour
    {
        static UIManager s_Instance;

        public static MainPanel MainPanel
        {
            get => s_Instance.m_MainPanel;
        }

        public static Vector2 ScaleFactor
        {
            get => GetScaleFactor();
        }

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

        InputAction m_PointerAction;

        void Awake()
        {
            s_Instance = this;
            Application.targetFrameRate = 144;
            m_UIDocument = GetComponent<UIDocument>();
            m_MainPanel = new MainPanel(this, m_UIDocument.rootVisualElement.Q<VisualElement>("MainPanel"));
            m_PointerAction = new InputAction(binding: "<Pointer>/position");
            m_PointerAction.Enable();
        }

        public static Vector2 GetScaleFactor()
        {
            var worldBound = s_Instance.uiDocument.rootVisualElement.worldBound;
            return new Vector2(worldBound.width / Screen.width, worldBound.height / Screen.height);
        }

        public static bool IsPointerOverControl()
        {
            var position = s_Instance.m_PointerAction.ReadValue<Vector2>();
            var element = s_Instance.uiDocument.rootVisualElement.panel.Pick(new Vector2(position.x, Screen.height - position.y) * ScaleFactor);
            return element != null;
        }
    }
}
