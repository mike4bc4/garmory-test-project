using System.Collections;
using System.Collections.Generic;
using Interactables;
using SchedulerUtility;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameUI
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
            // var worldBound = rootElement.panel.visualTree.worldBound;
            // var scaleFactor = new Vector2(worldBound.width / Screen.width, worldBound.height / Screen.height);
            // var screenPosition = CameraManager.MainCamera.WorldToScreenPoint(m_Transform.position);
            // rootElement.style.left = screenPosition.x * scaleFactor.x;
            // rootElement.style.top = (Screen.height - screenPosition.y) * scaleFactor.y;
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

            // foreach (var interactable in InteractableManager.Interactables)
            // {
            //     var pin = new Pin(interactable.transform) { text = interactable.state.description };
            //     rootElement.Add(pin);
            //     m_Pins.Add(pin);

            //     interactable.onStateChange += () =>
            //     {
            //         pin.text = interactable.state.description;
            //     };
            // }
        }
    }
}
