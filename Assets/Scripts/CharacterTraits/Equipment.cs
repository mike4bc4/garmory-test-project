using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterTraits
{
    public class Equipment
    {
        Character m_Character;

        public Character character
        {
            get => m_Character;
        }

        public Equipment(Character character)
        {
            m_Character = character;
        }
    }
}
