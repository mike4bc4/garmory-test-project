using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.CharacterUtility
{
    public class Character : MonoBehaviour
    {
        [SerializeField] MovementSystem.Settings m_MovementSystemSettings;
        [SerializeField] InteractableTracker.Settings m_InteractableTrackerSettings;

        [SerializeField] AttackSystem.Settings m_AttackSystemSettings;

        Inventory m_Inventory;

        public Inventory inventory
        {
            get => m_Inventory;
        }

        Equipment m_Equipment;

        public Equipment equipment
        {
            get => m_Equipment;
        }

        MovementSystem m_MovementSystem;

        public MovementSystem movementSystem
        {
            get => m_MovementSystem;
        }

        InteractableTracker m_InteractableTracker;

        public InteractableTracker interactableTracker
        {
            get => m_InteractableTracker;
        }

        AttackSystem m_AttackSystem;

        void Start()
        {
            m_Inventory = new Inventory(this);
            m_Equipment = new Equipment(this);
            m_MovementSystem = new MovementSystem(this, m_MovementSystemSettings);
            m_InteractableTracker = new InteractableTracker(this, m_InteractableTrackerSettings);
            m_AttackSystem = new AttackSystem(this, m_AttackSystemSettings);
        }

        void Update()
        {
            m_MovementSystem.Update();
        }
    }
}
