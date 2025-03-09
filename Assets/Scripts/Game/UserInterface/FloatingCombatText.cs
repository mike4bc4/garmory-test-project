using System.Collections;
using System.Collections.Generic;
using Game.CameraUtility;
using Game.UserInterface;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.UserInterface
{
    public class FloatingCombatText : FloatingControl
    {
        const string k_ClassName = "combat-text";

        Label m_Label;
        float m_Timer;
        Vector3 m_Position;

        public float time { get; set; }
        public float speed { get; set; }
        public float fadeTime { get; set; }
        public string text
        {
            get => m_Label.text;
            set => m_Label.text = value;
        }

        public FloatingCombatText(Transform transform) : base(transform)
        {
            rootElement.AddToClassList(k_ClassName);

            m_Label = new Label() { name = "Label" };
            rootElement.Add(m_Label);

            m_Position = transform.position;
            time = 1f;
            speed = 1f;
            fadeTime = 0.5f;
            text = "Combat Text";
        }

        protected override void Update()
        {
            m_Timer += Time.deltaTime;
            if (m_Timer > time)
            {
                rootElement.RemoveFromHierarchy();
            }

            float opacity = 1f;
            float fadeStartTime = time - fadeTime;
            if (m_Timer > fadeStartTime)
            {
                opacity = 1f - (m_Timer - fadeStartTime) / (fadeTime + Mathf.Epsilon);
            }

            rootElement.style.opacity = opacity;

            m_Position += Vector3.up * speed * Time.deltaTime;
            var scaleFactor = UserInterfaceManager.ScaleFactor;
            var screenPosition = CameraManager.Camera.WorldToScreenPoint(m_Position + offset);
            var screenRect = new Rect(0, 0, Screen.width, Screen.height);
            var display = screenRect.Contains(screenPosition) ? DisplayStyle.Flex : DisplayStyle.None;
            rootElement.style.display = display;
            if (display == DisplayStyle.Flex)
            {
                rootElement.style.left = screenPosition.x * scaleFactor.x;
                rootElement.style.top = (Screen.height - screenPosition.y) * scaleFactor.y;
            }
        }
    }
}
