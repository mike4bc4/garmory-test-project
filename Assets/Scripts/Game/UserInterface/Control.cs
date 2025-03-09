using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.UserInterface
{
    public abstract class Control
    {
        VisualElement m_RootElement;

        public VisualElement rootElement
        {
            get => m_RootElement;
            protected set => m_RootElement = value;
        }

        public Control()
        {
            m_RootElement = new VisualElement();
        }

        public Control(VisualElement rootElement)
        {
            m_RootElement = rootElement;
        }

        public static implicit operator VisualElement(Control control)
        {
            return control.rootElement;
        }
    }
}
