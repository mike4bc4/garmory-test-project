using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


namespace GameUI
{
    public abstract class SelfContainedControl
    {
        VisualElement m_RootElement;

        public VisualElement rootElement
        {
            get => m_RootElement;
        }

        public SelfContainedControl()
        {
            m_RootElement = new VisualElement();
        }

        public static implicit operator VisualElement(SelfContainedControl element)
        {
            return element.m_RootElement;
        }
    }
}
