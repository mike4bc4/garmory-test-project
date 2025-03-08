using System.Collections;
using System.Collections.Generic;
using Game.CameraUtility;
using Game.Interactables;
using SchedulerUtility;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.UserInterface
{
    public class Pin : SelfContainedControl
    {
        const string k_ClassName = "pin";

        Label m_Label;

        Transform m_Transform;

        public string text
        {
            get => m_Label.text;
            set => m_Label.text = value;
        }

        public Pin(Transform transform)
        {
            m_Transform = transform;
            rootElement.AddToClassList(k_ClassName);

            m_Label = new Label() { name = "PinLabel" };
            rootElement.Add(m_Label);

            Scheduler.OnUpdate += Track;
        }

        void Track()
        {
            var scaleFactor = UserInterfaceManager.ScaleFactor;
            var screenPosition = CameraManager.Camera.WorldToScreenPoint(m_Transform.position);
            var screenRect = new Rect(0, 0, Screen.width, Screen.height);
            var display = screenRect.Contains(screenPosition) ? DisplayStyle.Flex : DisplayStyle.None;
            rootElement.style.display = display;
            if (display == DisplayStyle.Flex)
            {
                rootElement.style.left = screenPosition.x * scaleFactor.x;
                rootElement.style.top = (Screen.height - screenPosition.y) * scaleFactor.y;
            }
        }
    }

    public class PinPanel : Control
    {
        class InteractablePin
        {
            public Interactable interactable;
            public Pin pin;
        }

        MainPanel m_MainPanel;

        public MainPanel mainPanel
        {
            get => m_MainPanel = mainPanel;
        }

        List<Pin> m_Pins;

        public PinPanel(MainPanel mainPanel, VisualElement rootElement) : base(rootElement)
        {
            m_MainPanel = mainPanel;
            m_Pins = new List<Pin>();
        }

        public Pin AddPin(Transform transform, string text)
        {
            var pin = new Pin(transform)
            {
                text = text,
            };

            m_Pins.Add(pin);
            rootElement.Add(pin);
            return pin;
        }
    }
}
