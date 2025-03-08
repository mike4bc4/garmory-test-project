using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.CharacterUtility;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.UserInterface
{
    public class EquipmentPanel : Control
    {
        CharacterRightPanel m_CharacterRightPanel;

        public CharacterRightPanel characterRightPanel
        {
            get => m_CharacterRightPanel;
        }

        GridContainer m_GridContainer;

        List<ItemSlotControl> m_ItemSlots;

        public DraggablePanel draggablePanel
        {
            get => m_CharacterRightPanel.characterPanel.mainPanel.draggablePanel;
        }

        public InventoryPanel inventoryPanel
        {
            get => m_CharacterRightPanel.characterPanel.leftPanel.inventoryPanel;
        }

        public Equipment m_Equipment;

        public Equipment equipment
        {
            get => m_Equipment;
            set => SetEquipment(value);
        }

        bool m_AllowDataChanges;
        bool m_AllowViewChanges;

        public EquipmentPanel(CharacterRightPanel characterRightPanel, VisualElement rootElement) : base(rootElement)
        {
            m_CharacterRightPanel = characterRightPanel;
            m_GridContainer = new GridContainer(rootElement.Q<VisualElement>("GridContainer"));
            m_GridContainer.SetSize(9, 3);
            m_GridContainer.SetCellsVisible(false, 0, 5, 6);

            m_ItemSlots = new List<ItemSlotControl>()
            {
                new ItemSlotControl() { category = EquipmentItems.Category.Helmet },
                new ItemSlotControl() { category = EquipmentItems.Category.Necklace },
                new ItemSlotControl() { category = EquipmentItems.Category.Weapon },
                new ItemSlotControl() { category = EquipmentItems.Category.Armor },
                new ItemSlotControl() { category = EquipmentItems.Category.Boots },
                new ItemSlotControl() { category = EquipmentItems.Category.Ring },
            };

            m_GridContainer[1].rootElement.Add(m_ItemSlots[0]);
            m_GridContainer[2].rootElement.Add(m_ItemSlots[1]);
            m_GridContainer[3].rootElement.Add(m_ItemSlots[2]);
            m_GridContainer[4].rootElement.Add(m_ItemSlots[3]);
            m_GridContainer[7].rootElement.Add(m_ItemSlots[4]);
            m_GridContainer[8].rootElement.Add(m_ItemSlots[5]);

            foreach (var itemSlot in m_ItemSlots)
            {
                // Updates equipment data when view has changed.
                itemSlot.onItemChanged += () =>
                {
                    if (m_AllowDataChanges)
                    {
                        // Disallow view changes, it's already up to date.
                        m_AllowViewChanges = false;
                        if (itemSlot.item != null)
                        {
                            equipment.Equip(itemSlot.item);
                        }
                        else
                        {
                            equipment.Unequip(itemSlot.category);
                        }
                        m_AllowViewChanges = true;
                    }
                };

                itemSlot.onDragStarted += () =>
                {
                    if (itemSlot.item != null)
                    {
                        draggablePanel.draggableControl = itemSlot;
                    }
                };

                itemSlot.onReleased += () =>
                {
                    if (draggablePanel.dragStoppedTimestamp == Time.time
                        && draggablePanel.previousDraggableControl is ItemSlotControl draggedItemSlot
                        && draggedItemSlot.item.category == itemSlot.category)
                    {
                        (itemSlot.item, draggedItemSlot.item) = (draggedItemSlot.item, itemSlot.item);
                        if (draggedItemSlot.selected)
                        {
                            inventoryPanel.selectedItemSlot = itemSlot;
                        }
                        else if (itemSlot.selected)
                        {
                            inventoryPanel.selectedItemSlot = draggedItemSlot;
                        }

                        // Refresh item description panel as dropped item might have been taken off from character.
                        inventoryPanel.RefreshItemDescriptionPanel();
                    }
                };

                itemSlot.onClicked += () => inventoryPanel.selectedItemSlot = itemSlot;
            }

            equipment = null;
            m_AllowDataChanges = true;
            m_AllowViewChanges = true;
        }

        public void SetEquipment(Equipment equipment)
        {
            if (m_Equipment != null)
            {
                m_Equipment.onChanged -= OnEquipmentChanged;
            }

            m_Equipment = equipment;
            if (m_Equipment != null)
            {
                m_Equipment.onChanged += OnEquipmentChanged;
            }

            RefreshEquipment();
        }

        /// <summary>
        /// Updates view when equipment data has changed.
        /// </summary>
        /// <param name="index"></param>
        void OnEquipmentChanged(int index)
        {
            if (m_AllowViewChanges)
            {
                // Disallow data changes, equipment is already up to date.
                m_AllowDataChanges = false;
                var itemSlot = m_ItemSlots[index];
                itemSlot.item = m_Equipment.items[index];
                m_AllowDataChanges = true;
            }
        }

        void RefreshEquipment()
        {
            // Disallow data changes, just want to update view.
            m_AllowDataChanges = false;
            for (int i = 0; i < m_ItemSlots.Count; i++)
            {
                ItemSlotControl itemSlot = m_ItemSlots[i];
                itemSlot.item = equipment?.items[i];
            }
            m_AllowDataChanges = true;
        }

        public ItemSlotControl GetItemSlot(EquipmentItems.Category category)
        {
            var index = (int)category;
            if (index >= 0 && index < m_ItemSlots.Count)
            {
                return m_ItemSlots[index];
            }

            return null;
        }

        public void OnItemSlotChanged(ItemSlotControl itemSlot)
        {
            throw new System.NotImplementedException();
        }
    }
}
