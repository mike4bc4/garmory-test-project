using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameUI.CustomControls
{
    public class CustomButton : VisualElement
    {
        const string k_UnityButtonClassName = "unity-button";

        public event Action onClicked;

        public new class UxmlFactory : UxmlFactory<CustomButton, UxmlTraits> { }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
            }
        }

        bool m_Pressed;

        public CustomButton()
        {
            AddToClassList(k_UnityButtonClassName);

            RegisterCallback<MouseDownEvent>(evt =>
            {
                m_Pressed = true;
            });

            RegisterCallback<MouseUpEvent>(evt =>
            {
                if (m_Pressed)
                {
                    onClicked?.Invoke();
                    m_Pressed = false;
                }
            });
        }
    }
}
