using System.Collections;
using System.Collections.Generic;
using Game.CameraUtility;
using SchedulerUtility;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.UserInterface
{
    public class FloatingPin : FloatingControl
    {
        Label m_Label;

        public string text
        {
            get => m_Label.text;
            set => m_Label.text = value;
        }

        public FloatingPin(Transform transform) : base(transform)
        {
            m_Label = new Label() { name = "PinLabel" };
            rootElement.Add(m_Label);
        }
    }
}
