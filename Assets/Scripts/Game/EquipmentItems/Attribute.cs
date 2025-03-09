using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.EquipmentItems
{
    public abstract class Attribute
    {
        AttributeType m_Type;

        public AttributeType type
        {
            get => m_Type;
        }

        string m_Name;

        public string name
        {
            get => m_Name;
        }

        float m_Value;

        public float value
        {
            get => m_Value;
            set => m_Value = value;
        }

        public bool isEmpty
        {
            get => m_Value == 0f;
        }

        public Attribute(string name, AttributeType type)
        {
            m_Name = name;
            m_Type = type;
        }

        public abstract string GetValueString(bool withSign = false);

        public abstract Attribute Clone();
    }

    public class NumericAttribute : Attribute
    {
        public NumericAttribute(string name, AttributeType type) : base(name, type) { }

        public override string GetValueString(bool withSign = false)
        {
            return withSign ? string.Format("{0:+0.#;-0.#}", value) : string.Format("{0:0.#}", value);
        }

        public override Attribute Clone()
        {
            return new NumericAttribute(name, type) { value = value };
        }
    }

    public class PercentageAttribute : Attribute
    {
        public PercentageAttribute(string name, AttributeType type) : base(name, type) { }

        public override string GetValueString(bool withSign = false)
        {
            return withSign ? string.Format("{0:+0.#%;-0.#%}", value / 100f) : string.Format("{0:0.#%}", value / 100f);
        }

        public override Attribute Clone()
        {
            return new PercentageAttribute(name, type) { value = value };
        }
    }
}
