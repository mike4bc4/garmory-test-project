using System.Collections;
using System.Collections.Generic;
using Game.EquipmentItems;
using UnityEngine;

namespace Game.CharacterUtility
{
    public class Character : MonoBehaviour
    {
        [SerializeField] MovementSystem.Settings m_MovementSystemSettings;
        [SerializeField] InteractableTracker.Settings m_InteractableTrackerSettings;
        [SerializeField] AttackSystem.Settings m_AttackSystemSettings;
        [SerializeField] AnimationSystem.Settings m_AnimationSystemSettings;

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

        public AttackSystem attackSystem
        {
            get => m_AttackSystem;
        }

        AnimationSystem m_AnimationSystem;

        public AnimationSystem animationSystem
        {
            get => m_AnimationSystem;
        }

        List<Attribute> m_Attributes;

        public IReadOnlyList<Attribute> attributes
        {
            get => m_Attributes;
        }

        void Start()
        {
            m_Inventory = new Inventory(this);
            m_Equipment = new Equipment(this);
            m_Equipment.onChanged += OnEquipmentChanged;

            m_MovementSystem = new MovementSystem(this, m_MovementSystemSettings);
            m_InteractableTracker = new InteractableTracker(this, m_InteractableTrackerSettings);
            m_AttackSystem = new AttackSystem(this, m_AttackSystemSettings);
            m_AnimationSystem = new AnimationSystem(this, m_AnimationSystemSettings);
            
            m_Attributes = Item.AvailableAttributes;
        }

        public Attribute GetAttribute(AttributeType type)
        {
            foreach (var attribute in m_Attributes)
            {
                if (attribute.type == type)
                {
                    return attribute;
                }
            }

            return null;
        }

        void OnEquipmentChanged(int itemIndex)
        {
            m_Attributes = Item.AvailableAttributes;
            foreach (var item in equipment.items)
            {
                if (item != null)
                {
                    for (int i = 0; i < item.attributes.Count; i++)
                    {
                        m_Attributes[i].value += item.attributes[i].value;
                    }
                }
            }
        }

        void Update()
        {
            m_MovementSystem.Update();
        }
    }
}
