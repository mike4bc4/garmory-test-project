using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interactables
{
    public class State
    {
        string m_Description;

        public string description
        {
            get => m_Description;
        }

        public State(string description)
        {
            m_Description = description;
        }
    }
}
