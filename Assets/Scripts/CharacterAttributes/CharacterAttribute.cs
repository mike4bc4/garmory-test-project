using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace CharacterFeatures
{
    public abstract class CharacterAttribute
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

        public CharacterAttribute(string name)
        {
            m_Name = name;
        }

        public abstract string GetValueString(bool withSign = false);

        public abstract CharacterAttribute Clone();
    }

    public class NumericCharacterAttribute : CharacterAttribute
    {
        public NumericCharacterAttribute(string name) : base(name) { }

        public override string GetValueString(bool withSign = false)
        {
            return withSign ? string.Format("{0:+0.#;-0.#}", value) : string.Format("{0:0.#}", value);
        }

        public override CharacterAttribute Clone()
        {
            return new NumericCharacterAttribute(name) { value = value };
        }
    }

    public class PercentageCharacterAttribute : CharacterAttribute
    {
        public PercentageCharacterAttribute(string name) : base(name) { }

        public override string GetValueString(bool withSign = false)
        {
            return withSign ? string.Format("{0:+0.#%;-0.#%}", value / 100f) : string.Format("{0:0.#%}", value / 100f);
        }

        public override CharacterAttribute Clone()
        {
            return new PercentageCharacterAttribute(name) { value = value };
        }
    }
}
