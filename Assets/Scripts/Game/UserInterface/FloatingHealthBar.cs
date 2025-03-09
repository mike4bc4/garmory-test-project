using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.UserInterface
{
    public class FloatingHealthBar : FloatingControl
    {
        const string k_ClassName = "floating-health-bar";
        const string k_BarContainerClassName = k_ClassName + "__bar-container";
        const string k_BarClassName = k_ClassName + "__bar";

        float m_MaxHealth;

        public float maxHealth
        {
            get => m_MaxHealth;
            set => SetMaxHealth(value);
        }

        float m_Health;

        public float health
        {
            get => m_Health;
            set => SetHealth(value);
        }

        VisualElement m_BarContainer;

        VisualElement m_Bar;

        public FloatingHealthBar(Transform transform) : base(transform)
        {
            rootElement.AddToClassList(k_ClassName);

            m_BarContainer = new VisualElement() { name = "BarContainer" };
            m_BarContainer.AddToClassList(k_BarContainerClassName);
            rootElement.Add(m_BarContainer);

            m_Bar = new VisualElement() { name = "Bar" };
            m_Bar.AddToClassList(k_BarClassName);
            m_BarContainer.Add(m_Bar);
        }

        public void SetMaxHealth(float maxHealth)
        {
            m_MaxHealth = maxHealth;
            UpdateBarWidth();
        }

        public void SetHealth(float health)
        {
            m_Health = health;
            UpdateBarWidth();
        }

        void UpdateBarWidth()
        {
            m_Bar.style.width = Length.Percent(maxHealth != 0f ? health / maxHealth * 100f : 0f);
        }
    }
}
