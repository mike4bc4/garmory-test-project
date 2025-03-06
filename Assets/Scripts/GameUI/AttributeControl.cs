using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CharacterFeatures;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameUI
{
    public class AttributeControl : SelfContainedControl
    {
        const string k_ClassName = "attribute-control";
        const string k_ImageClassName = k_ClassName + "__image";
        // const string k_ImageDamageClassName = k_ImageClassName + "--damage";
        // const string k_ImageHealthPointsClassName = k_ImageClassName + "--health-points";
        // const string k_ImageDefenseClassName = k_ImageClassName + "--defense";
        // const string k_ImageLifeStealClassName = k_ImageClassName + "--life-steal";
        // const string k_ImageCriticalStrikeChanceClassName = k_ImageClassName + "--critical-strike-chance";
        // const string k_ImageAttackSpeedClassName = k_ImageClassName + "--attack-speed";
        // const string k_ImageMovementSpeedClassName = k_ImageClassName + "--movement-speed";
        // const string k_ImageLuckClassName = k_ImageClassName + "--luck";
        const string k_LabelClassName = k_ClassName + "__label";
        const string k_ValueClassName = k_ClassName + "__value";

        VisualElement m_ImageElement;
        Label m_LabelElement;
        Label m_ValueElement;

        CharacterAttribute m_Attribute;

        public CharacterAttribute attribute
        {
            get => m_Attribute;
            set => SetAttribute(value);
        }

        public AttributeControl()
        {
            rootElement.AddToClassList(k_ClassName);

            m_ImageElement = new VisualElement();
            m_ImageElement.AddToClassList(k_ImageClassName);
            rootElement.Add(m_ImageElement);

            m_LabelElement = new Label();
            m_LabelElement.AddToClassList(k_LabelClassName);
            rootElement.Add(m_LabelElement);

            m_ValueElement = new Label();
            m_ValueElement.AddToClassList(k_ValueClassName);
            rootElement.Add(m_ValueElement);
        }

        public void SetAttribute(CharacterAttribute attribute)
        {
            m_ImageElement.ClearClassList();
            m_ImageElement.AddToClassList(k_ImageClassName);
            m_ImageElement.AddToClassList(k_ImageClassName + "--" + string.Join('-', attribute.name.Split(' ')).ToLower());
            m_LabelElement.text = attribute.name;
            m_ValueElement.text = attribute.GetValueString();
        }
    }
}
