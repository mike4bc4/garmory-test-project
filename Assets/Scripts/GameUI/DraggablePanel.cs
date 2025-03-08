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
            onPointerUp += OnPointerUp;
        }

        void OnPointerUp()
        {
            if (m_DraggableControl != null)
            {
                m_DragStoppedTimestamp = Time.time;
                onDragStopped?.Invoke();
            }

            SetDraggable(null);
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
                m_Draggable.AddToClassList(k_DraggableClassName);
                rootElement.Add(m_Draggable);
                Scheduler.OnUpdate += MoveDraggable;
            }
        }

        void MoveDraggable()
        {
            m_Draggable.style.left = pointerPosition.x;
            m_Draggable.style.top = pointerPosition.y;
        }
    }
}
