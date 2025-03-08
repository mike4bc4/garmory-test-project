using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class InteractableManager : MonoBehaviour
    {
        static InteractableManager s_Instance;

        public static IReadOnlyList<Interactable> Interactables
        {
            get => s_Instance.m_Interactables;
        }

        [SerializeField] List<Interactable> m_Interactables;

        void Awake()
        {
            s_Instance = this;
        }
    }
}
