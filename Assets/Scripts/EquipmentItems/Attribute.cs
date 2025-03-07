using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace EquipmentItems
{
    public abstract class Attribute
    {
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

        public Attribute(string name)
        {
            m_Name = name;
        }

        public abstract string GetValueString(bool withSign = false);

        public abstract Attribute Clone();
    }

    public class NumericAttribute : Attribute
    {
        public NumericAttribute(string name) : base(name) { }

        public override string GetValueString(bool withSign = false)
        {
            return withSign ? string.Format("{0:+0.#;-0.#}", value) : string.Format("{0:0.#}", value);
        }

        public override Attribute Clone()
        {
            return new NumericAttribute(name) { value = value };
        }
    }

    public class PercentageAttribute : Attribute
    {
        public PercentageAttribute(string name) : base(name) { }

        public override string GetValueString(bool withSign = false)
        {
            return withSign ? string.Format("{0:+0.#%;-0.#%}", value / 100f) : string.Format("{0:0.#%}", value / 100f);
        }

        public override Attribute Clone()
        {
            return new PercentageAttribute(name) { value = value };
        }
    }
}
