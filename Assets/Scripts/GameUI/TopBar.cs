using System;
using System.Collections;
using System.Collections.Generic;
using GameUI.CustomControls;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameUI
{
    public class TopBar : Control
    {
        public event Action onCloseButtonClicked;
        Label m_Label;
        Button m_CloseButton;

        public string label
        {
            get => m_Label.text;
            set => m_Label.text = value;
        }

        public TopBar(VisualElement rootElement) : base(rootElement)
        {
            m_Label = rootElement.Q<Label>("Label");
            m_CloseButton = rootElement.Q<Button>("CloseButton");

            if (m_CloseButton != null)
            {
                m_CloseButton.clicked += () => onCloseButtonClicked?.Invoke();
            }
        }
    }
}
