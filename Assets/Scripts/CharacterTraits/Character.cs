using System.Collections;
using System.Collections.Generic;
using EquipmentItems;
using Gameplay;
using UnityEngine;

namespace CharacterTraits
{
    public class Character : MonoBehaviour
    {
        [SerializeField] float m_Speed;

        public float speed
        {
            get => m_Speed;
        }

        [SerializeField] float m_JumpHeight;

        public float jumpHeight
        {
            get => m_JumpHeight;
        }

        [SerializeField] float m_Gravity;

        public float gravity
        {
            get => m_Gravity;
        }

        [SerializeField] float m_RotationSpeed;

        public float rotationSpeed
        {
            get => m_RotationSpeed;
        }

        [SerializeField] InteractableTracker.Settings m_InteractableTrackerSettings;

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

        void Awake()
        {
            m_Inventory = new Inventory(this);
            m_Equipment = new Equipment(this);

            // ItemsGenerator.Generate(items =>
            // {
            //     Debug.Log("Items generated");
            //     inventory.AddItems(items);
            // });

            m_MovementSystem = new MovementSystem(this);
            m_InteractableTracker = new InteractableTracker(this, m_InteractableTrackerSettings);
        }

        void Update()
        {
            m_MovementSystem.Update();
        }
    }
}
