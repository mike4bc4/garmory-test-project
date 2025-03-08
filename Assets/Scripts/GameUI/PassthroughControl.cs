using System;
using System.Collections;
using System.Collections.Generic;
using InputUtilities;
using SchedulerUtility;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace GameUI
{
    public class PassthroughControl : Control
    {
        public event Action onPointerMove;
        public event Action onPointerUp;
        public event Action onPointerDown;

        Vector2 m_PointerPosition;

        public Vector2 pointerPosition
        {
            get => m_PointerPosition;
        }

        Vector2 m_PreviousPointerPosition;
        Vector2 m_Delta;

        public Vector2 delta
        {
            get => m_Delta;
        }

        public PassthroughControl(VisualElement rootElement) : base(rootElement)
        {
            rootElement.pickingMode = PickingMode.Ignore;

            var moveAction = new InputAction(binding: "<Pointer>/position");
            moveAction.Enable();

            var pressAction = new InputAction(binding: "<Pointer>/press");
            pressAction.Enable();

            moveAction.performed += ctx =>
            {
                var worldBound = rootElement.panel.visualTree.worldBound;
                var scaleFactor = new Vector2(worldBound.width / Screen.width, worldBound.height / Screen.height);
                var pointerPosition = moveAction.ReadValue<Vector2>();
                m_PointerPosition = new Vector2(pointerPosition.x, Screen.height - pointerPosition.y) * scaleFactor;

                if (m_PointerPosition != m_PreviousPointerPosition)
                {
                    m_Delta = m_PointerPosition - m_PreviousPointerPosition;
                    onPointerMove?.Invoke();
                    m_PreviousPointerPosition = m_PointerPosition;
                }
            };

            pressAction.started += ctx =>
            {
                onPointerDown?.Invoke();
            };

            pressAction.canceled += ctx =>
            {
                onPointerUp?.Invoke();
            };
        }
    }
}
