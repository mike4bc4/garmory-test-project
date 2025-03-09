using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Objectives
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Objective Factory")]
    public class ObjectiveFactory : ScriptableObject
    {
        [SerializeField] string m_Header;

        public string header
        {
            get => m_Header;
        }

        [SerializeField] string m_Description;

        public string description
        {
            get => m_Description;
        }

        public Objective Create()
        {
            return new Objective()
            {
                header = m_Header,
                description = m_Description,
            };
        }
    }
}
