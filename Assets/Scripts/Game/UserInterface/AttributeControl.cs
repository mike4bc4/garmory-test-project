using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Game.EquipmentItems;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

namespace Game.UserInterface
{
    public class AttributeControl : Control
    {
        const string k_ClassName = "attribute-control";
        const string k_ImageClassName = k_ClassName + "__image";

        VisualElement m_ImageElement;
        Label m_NameLabel;
        Label m_ValueLabel;
        Label m_ValueChangeLabel;

        EquipmentItems.Attribute m_Attribute;

        public EquipmentItems.Attribute attribute
        {
            get => m_Attribute;
            set => SetAttribute(value);
        }

        EquipmentItems.Attribute m_CompareAttribute;

        public EquipmentItems.Attribute compareAttribute
        {
            get => m_CompareAttribute;
            set => SetCompareAttribute(value);
        }

        bool m_ValueChangeVisible;

        public bool valueChangeVisible
        {
            get => m_ValueChangeVisible;
            set => SetValueChangeVisible(value);
        }

        public AttributeControl()
        {
            var templateInstance = UserInterfaceManager.Instance.attributeControl.Instantiate();
            rootElement = templateInstance.hierarchy.Children().First();

            m_ImageElement = rootElement.Q("AttributeImage");
            m_NameLabel = rootElement.Q<Label>("NameLabel");
            m_ValueLabel = rootElement.Q<Label>("ValueLabel");
            m_ValueChangeLabel = rootElement.Q<Label>("ValueCompareLabel");

            valueChangeVisible = true;
            compareAttribute = null;
        }

        public void SetAttribute(EquipmentItems.Attribute attribute)
        {
            m_Attribute = attribute;
            m_ImageElement.ClearClassList();
            m_ImageElement.AddToClassList(k_ImageClassName + "--" + string.Join('-', attribute.name.Split(' ')).ToLower());
            m_NameLabel.text = attribute.name;
            m_ValueLabel.text = attribute.GetValueString();
            RefreshValueChangeLabel();
        }

        public void SetCompareAttribute(EquipmentItems.Attribute attribute)
        {
            m_CompareAttribute = attribute;
            RefreshValueChangeLabel();
        }

        public void SetValueChangeVisible(bool visible)
        {
            m_ValueChangeVisible = visible;
            m_ValueChangeLabel.style.display = visible ? DisplayStyle.Flex : DisplayStyle.None;
        }

        void RefreshValueChangeLabel()
        {
            if (m_Attribute == null)
            {
                return;
            }

            if (m_CompareAttribute == null)
            {
                m_ValueChangeLabel.text = $"({m_Attribute.GetValueString(true)})";
            }
            else
            {
                var attribute = m_Attribute.Clone();
                attribute.value = m_Attribute.value - m_CompareAttribute.value;
                m_ValueChangeLabel.text = $"({attribute.GetValueString(true)})";
            }
        }
    }
}
