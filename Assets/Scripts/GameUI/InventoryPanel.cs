using System;
using System.Collections;
using System.Collections.Generic;
using EquipmentItems;
using Gameplay;
using InputUtilities;
using ResourcesUtility;
using SchedulerUtility;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace GameUI
{
    public class InventoryPanel : OpenablePanel
    {
        CharacterLeftPanel m_CharacterLeftPanel;

        public CharacterLeftPanel characterLeftPanel
        {
            get => m_CharacterLeftPanel;
        }

        TopBar m_TopBar;

        public TopBar topBar
        {
            get => m_TopBar;
        }

        GridContainer m_GridContainer;
        List<ItemSlotControl> m_ItemSlots;

        ItemDescriptionPanel m_DescriptionPanel;

        public ItemDescriptionPanel descriptionPanel
        {
            get => m_DescriptionPanel;
        }

        ItemSlotControl m_SelectedItemSlot;

        public ItemSlotControl selectedItemSlot
        {
            get => m_SelectedItemSlot;
            set => SetSelectedItemSlot(value);
        }

        public EquipmentPanel equipmentPanel
        {
            get => characterLeftPanel.characterPanel.rightPanel.equipmentPanel;
        }

        public InventoryPanel(CharacterLeftPanel characterLeftPanel, VisualElement rootElement) : base(rootElement)
        {
            m_CharacterLeftPanel = characterLeftPanel;
            m_TopBar = new TopBar(rootElement.Q<VisualElement>("TopBar"));
            m_TopBar.onCloseButtonClicked += Close;

            m_GridContainer = new GridContainer(rootElement.Q<VisualElement>("GridContainer"));
            m_ItemSlots = new List<ItemSlotControl>();

            m_DescriptionPanel = new ItemDescriptionPanel(rootElement.Q<VisualElement>("DescriptionPanel"));

            Close();

            //----
            m_GridContainer.SetSize(100, 4);

            foreach (var cell in m_GridContainer.cells)
            {
                var itemSlot = new ItemSlotControl();
                cell.rootElement.Add(itemSlot);
                m_ItemSlots.Add(itemSlot);
            }

            ItemsGenerator.Generate(items =>
            {
                for (int i = 0; i < items.Count; i++)
                {
                    m_ItemSlots[i].item = items[i];
                }

                var mainPanel = characterLeftPanel.characterPanel.mainPanel;
                foreach (var itemSlot in m_ItemSlots)
                {
                    itemSlot.onDragStarted += () =>
                    {
                        if (itemSlot.item != null)
                        {
                            mainPanel.draggablePanel.draggableControl = itemSlot;
                        }
                    };

                    itemSlot.onReleased += () =>
                    {
                        if (mainPanel.draggablePanel.dragStoppedTimestamp == Time.time
                            && mainPanel.draggablePanel.previousDraggableControl is ItemSlotControl draggedItemSlot)
                        {
                            (itemSlot.item, draggedItemSlot.item) = (draggedItemSlot.item, itemSlot.item);
                            if (draggedItemSlot.selected)
                            {
                                itemSlot.Click();
                            }

                            // Refresh item description panel as dropped item might have been taken off from character.
                            RefreshItemDescriptionPanel();
                        }
                    };

                    itemSlot.onClicked += () => SetSelectedItemSlot(itemSlot);
                }
            });
        }

        public void RefreshItemDescriptionPanel()
        {
            if (m_SelectedItemSlot != null)
            {
                descriptionPanel.item = m_SelectedItemSlot.item;
                descriptionPanel.compareItem = equipmentPanel.GetItemSlot(m_SelectedItemSlot.item.category)?.item;
            }
        }

        public void SetSelectedItemSlot(ItemSlotControl selectedItemSlot)
        {
            var previousSelectedItemSlot = m_SelectedItemSlot;

            m_SelectedItemSlot = null;
            if (previousSelectedItemSlot != null)
            {
                previousSelectedItemSlot.selected = false;
            }

            descriptionPanel.item = null;
            descriptionPanel.compareItem = null;

            if (previousSelectedItemSlot != selectedItemSlot && selectedItemSlot.item != null)
            {
                m_SelectedItemSlot = selectedItemSlot;
                selectedItemSlot.selected = true;
                descriptionPanel.item = selectedItemSlot.item;
                descriptionPanel.compareItem = equipmentPanel.GetItemSlot(selectedItemSlot.item.category)?.item;
            }
        }
    }
}
