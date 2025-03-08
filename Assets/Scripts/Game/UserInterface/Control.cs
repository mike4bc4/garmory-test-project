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
        }

        public Control(VisualElement rootElement)
        {
            m_RootElement = rootElement;
        }
    }
}
