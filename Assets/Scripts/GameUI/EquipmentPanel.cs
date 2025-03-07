using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameUI
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
                            itemSlot.Click();
                        }

                        // Refresh item description panel as dropped item might have been taken off from character.
                        inventoryPanel.RefreshItemDescriptionPanel();
                    }
                };

                itemSlot.onClicked += () => inventoryPanel.SetSelectedItemSlot(itemSlot);
            }
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
    }
}
