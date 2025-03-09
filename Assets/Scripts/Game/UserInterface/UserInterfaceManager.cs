using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Game.UserInterface
{
    public class UserInterfaceManager : MonoBehaviour
    {
        static UserInterfaceManager s_Instance;

        public static UserInterfaceManager Instance
        {
            get => s_Instance;
        }

        public static MainPanel MainPanel
        {
            get => s_Instance.m_MainPanel;
        }

        public static Vector2 ScaleFactor
        {
            get => GetScaleFactor();
        }

        public static Vector2 PointerPosition
        {
            get => GetPointerPosition();
        }

        [SerializeField] VisualTreeAsset m_AttributeControl;

        public VisualTreeAsset attributeControl
        {
            get => m_AttributeControl;
        }

        [SerializeField] VisualTreeAsset m_ItemTooltipControl;

        public VisualTreeAsset itemTooltipControl
        {
            get => m_ItemTooltipControl;
        }

        [SerializeField] VisualTreeAsset m_WelcomePanel;

        public VisualTreeAsset welcomePanel
        {
            get => m_WelcomePanel;
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

        public static Vector2 GetPointerPosition()
        {
            var position = s_Instance.m_PointerAction.ReadValue<Vector2>();
            return new Vector2(position.x, Screen.height - position.y) * ScaleFactor;
        }

        public static Vector2 GetScaleFactor()
        {
            var worldBound = s_Instance.uiDocument.rootVisualElement.worldBound;
            return new Vector2(worldBound.width / Screen.width, worldBound.height / Screen.height);
        }

        public static bool IsPointerOverControl()
        {
            return s_Instance.uiDocument.rootVisualElement.panel.Pick(GetPointerPosition()) != null;
        }
    }
}
