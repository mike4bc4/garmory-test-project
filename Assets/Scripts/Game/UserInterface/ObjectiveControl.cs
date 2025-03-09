using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.UserInterface
{
    public class ObjectiveControl : SelfContainedControl
    {
        const string k_ClassName = "objective";
        const string k_HeaderClassName = k_ClassName + "__header";
        const string k_DescriptionClassName = k_ClassName + "__description";

        Label m_Header;

        Label m_Description;

        public string headerText
        {
            get => m_Header.text;
            set => m_Header.text = value;
        }

        public string descriptionText
        {
            get => m_Description.text;
            set => m_Description.text = value;
        }

        public ObjectiveControl()
        {
            rootElement.AddToClassList(k_ClassName);
            rootElement.pickingMode = PickingMode.Ignore;

            m_Header = new Label() { name = "Header" };
            m_Header.AddToClassList(k_HeaderClassName);
            m_Header.pickingMode = PickingMode.Ignore;
            rootElement.Add(m_Header);

            m_Description = new Label() { name = "description" };
            m_Description.AddToClassList(k_DescriptionClassName);
            m_Description.pickingMode = PickingMode.Ignore;
            rootElement.Add(m_Description);
        }
    }
}
