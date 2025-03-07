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

                m_ItemSlots[1].selected = true;
                descriptionPanel.slotControl = m_ItemSlots[1];

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
                        }
                    };

                    itemSlot.onClicked += () =>
                    {
                        var previousDescriptionPanelSlotControl = descriptionPanel.slotControl;
                        if (descriptionPanel.slotControl != null)
                        {
                            descriptionPanel.slotControl.selected = false;
                            descriptionPanel.slotControl = null;
                        }

                        if (previousDescriptionPanelSlotControl != itemSlot && itemSlot.item != null)
                        {
                            itemSlot.selected = true;
                            descriptionPanel.slotControl = itemSlot;
                        }
                    };
                }
            });
        }
    }
}
