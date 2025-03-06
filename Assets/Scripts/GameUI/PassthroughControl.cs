using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameUI
{
    public class PassthroughControl : Control
    {
        VisualElement m_PreviousElementUnderneath;

        bool m_Enabled;

        public bool enabled
        {
            get => m_Enabled;
            set => SetEnabled(value);
        }

        public PassthroughControl(VisualElement rootElement) : base(rootElement)
        {
            SetEnabled(true);
        }

        void OnMouseDown(MouseDownEvent evt)
        {
            var elementUnderneath = GetElementUnderneath(evt.mousePosition);
            if (elementUnderneath != null)
            {
                var newEvent = new Event();
                newEvent.type = EventType.MouseDown;
                newEvent.mousePosition = evt.mousePosition;
                using (MouseDownEvent mouseDownEvent = MouseDownEvent.GetPooled(newEvent))
                {
                    mouseDownEvent.target = elementUnderneath;
                    elementUnderneath.SendEvent(mouseDownEvent);
                }
            }
        }

        void OnMouseUp(MouseUpEvent evt)
        {
            var elementUnderneath = GetElementUnderneath(evt.mousePosition);
            if (elementUnderneath != null)
            {
                var newEvent = new Event();
                newEvent.type = EventType.MouseUp;
                newEvent.mousePosition = evt.mousePosition;
                using (MouseUpEvent mouseUpEvent = MouseUpEvent.GetPooled(newEvent))
                {
                    mouseUpEvent.target = elementUnderneath;
                    elementUnderneath.SendEvent(mouseUpEvent);
                }
            }
        }

        void OnMouseMove(MouseMoveEvent evt)
        {
            var elementUnderneath = GetElementUnderneath(evt.mousePosition);
            if (elementUnderneath != null)
            {
                {
                    var newEvent = new Event();
                    newEvent.type = EventType.MouseMove;
                    newEvent.mousePosition = evt.mousePosition;
                    using (MouseMoveEvent mouseMoveEvent = MouseMoveEvent.GetPooled(newEvent))
                    {
                        mouseMoveEvent.target = elementUnderneath;
                        elementUnderneath.SendEvent(mouseMoveEvent);
                    }
                }

                if (elementUnderneath != m_PreviousElementUnderneath)
                {
                    var newEvent = new Event();
                    newEvent.type = EventType.MouseMove;
                    newEvent.mousePosition = evt.mousePosition;
                    using (MouseEnterEvent mouseEnterEvent = MouseEnterEvent.GetPooled(newEvent))
                    {
                        mouseEnterEvent.target = elementUnderneath;
                        elementUnderneath.SendEvent(mouseEnterEvent);
                    }
                }
            }

            if (elementUnderneath != m_PreviousElementUnderneath && m_PreviousElementUnderneath != null)
            {
                var newEvent = new Event();
                newEvent.type = EventType.MouseMove;
                newEvent.mousePosition = evt.mousePosition;
                using (MouseLeaveEvent mouseLeaveEvent = MouseLeaveEvent.GetPooled(newEvent))
                {
                    mouseLeaveEvent.target = m_PreviousElementUnderneath;
                    m_PreviousElementUnderneath.SendEvent(mouseLeaveEvent);
                }
            }

            m_PreviousElementUnderneath = elementUnderneath;
        }

        void OnWheelEvent(WheelEvent evt)
        {
            var elementUnderneath = GetElementUnderneath(evt.mousePosition);
            if (elementUnderneath != null)
            {
                var newEvent = new Event();
                newEvent.type = EventType.MouseUp;
                newEvent.mousePosition = evt.mousePosition;
                newEvent.delta = evt.delta;
                using (WheelEvent wheelEvent = WheelEvent.GetPooled(newEvent))
                {
                    wheelEvent.target = elementUnderneath;
                    elementUnderneath.SendEvent(wheelEvent);
                }
            }
        }

        public void SetEnabled(bool enabled)
        {
            m_Enabled = enabled;
            if (enabled)
            {
                rootElement.RegisterCallback<MouseDownEvent>(OnMouseDown);
                rootElement.RegisterCallback<MouseUpEvent>(OnMouseUp);
                rootElement.RegisterCallback<MouseMoveEvent>(OnMouseMove);
                rootElement.RegisterCallback<WheelEvent>(OnWheelEvent);
            }
            else
            {
                rootElement.UnregisterCallback<MouseDownEvent>(OnMouseDown);
                rootElement.UnregisterCallback<MouseUpEvent>(OnMouseUp);
                rootElement.UnregisterCallback<MouseMoveEvent>(OnMouseMove);
                rootElement.UnregisterCallback<WheelEvent>(OnWheelEvent);
            }
        }

        VisualElement GetElementUnderneath(Vector2 position)
        {
            var pickedElements = new List<VisualElement>();
            rootElement.panel.PickAll(position, pickedElements);
            if (pickedElements.Count > 1 && pickedElements[0] == rootElement)
            {
                return pickedElements[1];
            }

            return null;
        }
    }
}
