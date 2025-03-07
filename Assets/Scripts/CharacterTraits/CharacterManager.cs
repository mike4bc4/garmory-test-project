using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterTraits
{
    public class CharacterManager : MonoBehaviour
    {
        static CharacterManager s_Instance;

        public static Character MainCharacter
        {
            get => s_Instance.m_MainCharacter;
        }

        [SerializeField] Character m_MainCharacter;

        void Awake()
        {
            s_Instance = this;
        }
    }
}
