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
        List<InventorySlotControl> m_SlotControls;

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
            m_SlotControls = new List<InventorySlotControl>();

            m_DescriptionPanel = new ItemDescriptionPanel(rootElement.Q<VisualElement>("DescriptionPanel"));

            Close();

            //----
            m_GridContainer.SetSize(100, 4);

            foreach (var cell in m_GridContainer.cells)
            {
                var itemControl = new InventorySlotControl();
                cell.rootElement.Add(itemControl);
                m_SlotControls.Add(itemControl);
            }

            ItemsGenerator.Generate(items =>
            {
                for (int i = 0; i < items.Count; i++)
                {
                    m_SlotControls[i].item = items[i];
                }

                m_SlotControls[1].selected = true;
                descriptionPanel.slotControl = m_SlotControls[1];

                var mainPanel = characterLeftPanel.characterPanel.mainPanel;
                foreach (var slotControl in m_SlotControls)
                {
                    slotControl.onDragStarted += () =>
                    {
                        if (slotControl.item != null)
                        {
                            mainPanel.draggablePanel.draggableControl = slotControl;
                            Debug.Log("Picked " + slotControl.item.name);
                        }
                    };

                    slotControl.onReleased += () =>
                    {
                        if (mainPanel.draggablePanel.dragStoppedTimestamp == Time.time
                            && mainPanel.draggablePanel.previousDraggableControl is InventorySlotControl droppedSlotControl)
                        {
                            Debug.Log("Dropped " + droppedSlotControl.item.name + " on " + (slotControl.item != null ? slotControl.item.name : "empty slot"));
                        }
                    };

                    slotControl.onClicked += () =>
                    {
                        var previousDescriptionPanelSlotControl = descriptionPanel.slotControl;
                        if (descriptionPanel.slotControl != null)
                        {
                            descriptionPanel.slotControl.selected = false;
                            descriptionPanel.slotControl = null;
                        }

                        if (previousDescriptionPanelSlotControl != slotControl && slotControl.item != null)
                        {
                            slotControl.selected = true;
                            descriptionPanel.slotControl = slotControl;
                        }
                    };
                }
            });
        }
    }
}
