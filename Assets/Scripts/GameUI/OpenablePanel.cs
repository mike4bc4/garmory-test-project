using System.Collections;
using System.Collections.Generic;
using GameUI;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameUI
{
    public class OpenablePanel : Control
    {
        protected bool m_IsOpen;

        public bool isOpen
        {
            get => m_IsOpen;
        }

        public OpenablePanel(VisualElement rootElement) : base(rootElement)
        {
        }

        public virtual void Open()
        {
            m_IsOpen = true;
            rootElement.style.display = DisplayStyle.Flex;
        }

        public virtual void Close()
        {
            m_IsOpen = false;
            rootElement.style.display = DisplayStyle.None;
        }
    }
}
