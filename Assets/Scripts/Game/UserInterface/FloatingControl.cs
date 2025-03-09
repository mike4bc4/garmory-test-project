using System.Collections;
using System.Collections.Generic;
using Game.CameraUtility;
using SchedulerUtility;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.UserInterface
{
    public class FloatingControl : SelfContainedControl
    {
        const string k_ClassName = "floating-control";

        Transform m_Transform;

        public Vector3 offset { get; set; }

        public FloatingControl(Transform transform)
        {
            rootElement.AddToClassList(k_ClassName);
            m_Transform = transform;

            rootElement.RegisterCallback<AttachToPanelEvent>(evt =>
            {
                Scheduler.OnUpdate += Update;
            });

            rootElement.RegisterCallback<DetachFromPanelEvent>(evt =>
            {
                Scheduler.OnUpdate -= Update;
            });

            rootElement.style.display = DisplayStyle.None;
        }

        protected virtual void Update()
        {
            var scaleFactor = UserInterfaceManager.ScaleFactor;
            var screenPosition = CameraManager.Camera.WorldToScreenPoint(m_Transform.position + offset);
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
}