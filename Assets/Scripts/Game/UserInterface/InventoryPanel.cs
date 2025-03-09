using System;
using System.Collections;
using System.Collections.Generic;
using Game.CharacterUtility;
using InputUtility;
using SchedulerUtility;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Game.UserInterface
{
    public class InventoryPanel : OpenablePanel
    {
        const int k_GridContainerRowSize = 4;

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

        public EquipmentPanel equipmentPanel
        {
            get => characterLeftPanel.characterPanel.rightPanel.equipmentPanel;
        }

        public DraggablePanel draggablePanel
        {
            get => characterLeftPanel.characterPanel.mainPanel.draggablePanel;
        }

        public ItemTooltipPanel itemTooltipPanel
        {
            get => characterLeftPanel.characterPanel.mainPanel.itemTooltipPanel;
        }

        Inventory m_Inventory;

        public Inventory inventory
        {
            get => m_Inventory;
            set => SetInventory(value);
        }

        bool m_AllowDataChanges;
        bool m_AllowViewChanges;

        public InventoryPanel(CharacterLeftPanel characterLeftPanel, VisualElement rootElement) : base(rootElement)
        {
            m_CharacterLeftPanel = characterLeftPanel;
            m_TopBar = new TopBar(rootElement.Q<VisualElement>("TopBar"));
            m_TopBar.onCloseButtonClicked += Close;

            m_GridContainer = new GridContainer(rootElement.Q<VisualElement>("GridContainer"));
            m_ItemSlots = new List<ItemSlotControl>();

            Close();
            inventory = null;
            m_AllowDataChanges = true;
            m_AllowViewChanges = true;
        }

        public void SetInventory(Inventory inventory)
        {
            Clear();
            if (m_Inventory != null)
            {
                m_Inventory.onChanged -= OnInventoryChanged;
                m_Inventory.onSizeChanged -= RebuildInventory;
            }

            m_Inventory = inventory;
            if (m_Inventory != null)
            {
                m_Inventory.onChanged += OnInventoryChanged;
                m_Inventory.onSizeChanged += RebuildInventory;
                RebuildInventory();
            }
        }

        /// <summary>
        /// Updates view when inventory data has changed.
        /// </summary>
        void OnInventoryChanged(List<int> changedIndexes)
        {
            if (m_AllowViewChanges)
            {
                foreach (var index in changedIndexes)
                {
                    // Disallow data changes, inventory is already up to date.
                    m_AllowDataChanges = false;
                    var itemSlot = m_ItemSlots[index];
                    itemSlot.item = m_Inventory.items[index];
                    m_AllowDataChanges = true;
                }
            }
        }

        void Clear()
        {
            m_GridContainer.Clear();
            foreach (var itemSlot in m_ItemSlots)
            {
                itemSlot.OnDestroy();
            }

            m_ItemSlots.Clear();
        }

        void RebuildInventory()
        {
            Clear();
            m_GridContainer.SetSize(m_Inventory.size, k_GridContainerRowSize);
            for (int i = 0; i < m_GridContainer.cells.Count; i++)
            {
                var itemSlotIndex = i;
                var itemSlot = new ItemSlotControl();
                m_GridContainer[i].rootElement.Add(itemSlot);
                m_ItemSlots.Add(itemSlot);
                itemSlot.item = inventory.items[i];

                // Updates inventory data when view has changed.
                itemSlot.onItemChanged += () =>
                {
                    if (m_AllowDataChanges)
                    {
                        // Disallow view changes, UI is already up to date.
                        m_AllowViewChanges = false;
                        m_Inventory.PutItem(itemSlotIndex, itemSlot.item);
                        m_AllowViewChanges = true;
                    }
                };

                itemSlot.onDragStarted += () =>
                {
                    if (itemSlot.item != null)
                    {
                        draggablePanel.draggableControl = itemSlot;
                    }

                    itemTooltipPanel.tooltip.item = null;
                    itemTooltipPanel.tooltip.compareItem = null;
                };

                itemSlot.onReleased += () =>
                {
                    if (draggablePanel.dragStoppedTimestamp == Time.time
                        && draggablePanel.previousDraggableControl is ItemSlotControl draggedItemSlot)
                    {
                        (itemSlot.item, draggedItemSlot.item) = (draggedItemSlot.item, itemSlot.item);
                        itemTooltipPanel.tooltip.item = itemSlot.item;
                        itemTooltipPanel.tooltip.compareItem = itemSlot.item != null ? equipmentPanel.GetItemSlot(itemSlot.item.category)?.item : null;
                    }
                };

                itemSlot.onMove += () =>
                {
                    if (draggablePanel.draggableControl == null)
                    {
                        itemTooltipPanel.tooltip.item = itemSlot.item;
                        itemTooltipPanel.tooltip.compareItem = itemSlot.item != null ? equipmentPanel.GetItemSlot(itemSlot.item.category)?.item : null;
                    }
                };

                itemSlot.onLeave += () =>
                {
                    itemTooltipPanel.tooltip.item = null;
                    itemTooltipPanel.tooltip.compareItem = null;
                };
            }
        }
    }
}
