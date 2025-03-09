using System.Collections;
using System.Collections.Generic;
using Game.UserInterface;
using UnityEngine;

namespace Game.Objectives
{
    public class ObjectiveManager : MonoBehaviour
    {
        static ObjectiveManager s_Instance;

        public static ObjectiveManager Instance
        {
            get => s_Instance;
        }

        [SerializeField] ObjectiveFactory m_MainObjectiveFactory;

        Objective m_MainObjective;

        void Awake()
        {
            s_Instance = this;
        }

        void Start()
        {
            m_MainObjective = m_MainObjectiveFactory.Create();
        }
    }
}
