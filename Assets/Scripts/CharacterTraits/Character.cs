using System.Collections;
using System.Collections.Generic;
using EquipmentItems;
using UnityEngine;

namespace CharacterTraits
{
    public class Character : MonoBehaviour
    {
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

        void Awake()
        {
            m_Inventory = new Inventory(this);
            m_Equipment = new Equipment(this);
        }
    }
}
