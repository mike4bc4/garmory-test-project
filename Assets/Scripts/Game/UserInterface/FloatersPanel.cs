using System.Collections;
using System.Collections.Generic;
using Game.CameraUtility;
using Game.Interactables;
using SchedulerUtility;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.UserInterface
{
    public class FloatersPanel : Control
    {
        class InteractablePin
        {
            public Interactable interactable;
            public FloatingPin pin;
        }

        MainPanel m_MainPanel;

        public MainPanel mainPanel
        {
            get => m_MainPanel = mainPanel;
        }

        List<FloatingControl> m_FloatingControls;

        public FloatersPanel(MainPanel mainPanel, VisualElement rootElement) : base(rootElement)
        {
            m_MainPanel = mainPanel;
            m_FloatingControls = new List<FloatingControl>();
        }

        public FloatingPin AddPin(Transform transform, string text)
        {
            var pin = new FloatingPin(transform)
            {
                text = text,
            };

            m_FloatingControls.Add(pin);
            rootElement.Add(pin);
            return pin;
        }

        public FloatingHealthBar AddHealthBar(Transform transform)
        {
            var healthBar = new FloatingHealthBar(transform);
            m_FloatingControls.Add(healthBar);
            rootElement.Add(healthBar);
            return healthBar;
        }

        public void RemoveHealthBar(FloatingHealthBar healthBar)
        {
            m_FloatingControls.Remove(healthBar);
            rootElement.Remove(healthBar);
        }

        public FloatingCombatText AddCombatText(Transform transform)
        {
            var combatText = new FloatingCombatText(transform);
            rootElement.Add(combatText);
            return combatText;
        }
    }
}
