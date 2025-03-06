using System;
using System.Collections;
using System.Collections.Generic;
using EquipmentItems;
using InputUtilities;
using SchedulerUtility;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace GameUI
{
    public class DraggablePanel : PassthroughControl
    {
        const string k_DraggableClassName = "draggable-panel__draggable";

        public event Action onDragStopped;

        MainPanel m_MainPanel;

        public MainPanel mainPanel
        {
            get => m_MainPanel;
        }

        Vector2 m_MousePosition;
        IDraggableControl m_DraggableControl;

        public IDraggableControl draggableControl
        {
            get => m_DraggableControl;
            set => SetDraggable(value);
        }

        IDraggableControl m_PreviousDraggableControl;

        public IDraggableControl previousDraggableControl
        {
            get => m_PreviousDraggableControl;
        }

        VisualElement m_Draggable;

        float m_DragStoppedTimestamp;

        public float dragStoppedTimestamp
        {
            get => m_DragStoppedTimestamp;
        }

        public DraggablePanel(MainPanel mainPanel, VisualElement rootElement) : base(rootElement)
        {
            m_MainPanel = mainPanel;
            rootElement.RegisterCallback<MouseMoveEvent>(OnMouseMove);
            rootElement.RegisterCallback<MouseUpEvent>(OnMouseUp);
        }

        void OnMouseUp(MouseUpEvent evt)
        {
            if (m_DraggableControl != null)
            {
                m_DragStoppedTimestamp = Time.time;
                onDragStopped?.Invoke();
            }

            SetDraggable(null);
        }

        void OnMouseMove(MouseMoveEvent evt)
        {
            m_MousePosition = evt.mousePosition;
        }

        public void SetDraggable(IDraggableControl draggableControl)
        {
            m_PreviousDraggableControl = m_DraggableControl;
            if (m_DraggableControl != null)
            {
                m_DraggableControl.OnDestroyDraggable(m_Draggable);
                rootElement.Remove(m_Draggable);
                Scheduler.OnUpdate -= MoveDraggable;
                m_DraggableControl = null;
                m_Draggable = null;
            }

            m_DraggableControl = draggableControl;
            if (m_DraggableControl != null)
            {
                m_Draggable = draggableControl.CreateDraggable();
                SetDraggablePickingMode(PickingMode.Ignore);
                m_Draggable.AddToClassList(k_DraggableClassName);
                rootElement.Add(m_Draggable);
                Scheduler.OnUpdate += MoveDraggable;
            }
        }

        void SetDraggablePickingMode(PickingMode pickingMode)
        {
            var elements = m_Draggable.Query<VisualElement>().ToList();
            foreach (var element in elements)
            {
                element.pickingMode = pickingMode;
            }
        }

        void MoveDraggable()
        {
            m_Draggable.style.left = m_MousePosition.x;
            m_Draggable.style.top = m_MousePosition.y;
        }
    }
}
