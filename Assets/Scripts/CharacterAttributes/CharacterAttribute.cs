using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterFeatures
{
    public abstract class CharacterAttribute
    {
        string m_Name;

        public string name
        {
            get => m_Name;
        }

        public CharacterAttribute(string name)
        {
            m_Name = name;
        }

        public abstract string GetValueString();

        public abstract bool isEmpty { get; }
    }

    public class CharacterAttribute<T> : CharacterAttribute
    {
        T m_Value;

        public T value
        {
            get => m_Value;
        }

        public override bool isEmpty
        {
            get
            {
                if (value is int intValue)
                {
                    return intValue == 0;
                }
                else if (value is float floatValue)
                {
                    return floatValue == 0f;
                }

                return false;
            }
        }

        Func<T, string> m_ValueParseFunction;

        public CharacterAttribute(string name, T value, Func<T, string> valueParseFunction) : base(name)
        {
            m_Value = value;
            m_ValueParseFunction = valueParseFunction;
        }

        public override string GetValueString()
        {
            return m_ValueParseFunction(value);
        }
    }
}
