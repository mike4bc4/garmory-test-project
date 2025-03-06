using System;
using System.Collections;
using System.Collections.Generic;
using EquipmentItems;
using GameUI;
using ResourcesUtility;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

namespace GameUI
{
    public class InventorySlotControl : SelfContainedControl, IDraggableControl
    {
        const float k_DragRadius = 10f;
        const string k_ClassName = "inventory-item-box";
        const string k_BackgroundIconClassName = "inventory-item-box__background";
        const string k_RarityIconClassName = "inventory-item-box__rarity";
        const string k_RarityIconCommonClassName = "inventory-item-box__rarity--common";
        const string k_RarityIconUncommonClassName = "inventory-item-box__rarity--uncommon";
        const string k_RarityIconRareClassName = "inventory-item-box__rarity--rare";
        const string k_RarityIconEpicClassName = "inventory-item-box__rarity--epic";
        const string k_RarityIconLegendaryClassName = "inventory-item-box__rarity--legendary";
        const string k_ItemIconClassName = "inventory-item-box__item";
        const string k_SelectedImageClassName = "inventory-item-box__selected-image";
        const string k_SelectedImageSelectedClassName = "inventory-item-box__selected-image--selected";

        public event Action onReleased;
        public event Action onClicked;
        public event Action onDragStarted;

        VisualElement m_BackgroundImage;
        VisualElement m_RarityImage;
        VisualElement m_ItemImage;
        VisualElement m_SelectedImage;
        Vector2 m_PressedPosition;
        bool m_Dragged;
        bool m_Pressed;
        
        bool m_Selected;

        public bool selected
        {
            get => m_Selected;
            set => SetSelected(value);
        }

        Item m_Item;

        public Item item
        {
            get => m_Item;
            set => SetItem(value);
        }

        bool m_EventsEnabled;

        public bool eventsEnabled
        {
            get => m_EventsEnabled;
            set => SetEventsEnabled(value);
        }

        AsyncOperationHandle<Texture2D> itemImageLoadHandle;

        public InventorySlotControl()
        {
            rootElement.AddToClassList(k_ClassName);
            SetEventsEnabled(true);

            m_BackgroundImage = new VisualElement();
            m_BackgroundImage.AddToClassList(k_BackgroundIconClassName);
            m_BackgroundImage.pickingMode = PickingMode.Ignore;
            rootElement.Add(m_BackgroundImage);

            m_RarityImage = new VisualElement();
            m_RarityImage.pickingMode = PickingMode.Ignore;
            m_BackgroundImage.AddToClassList(k_RarityIconClassName);
            m_BackgroundImage.AddToClassList(k_RarityIconCommonClassName);
            rootElement.Add(m_RarityImage);

            m_ItemImage = new VisualElement() { name = "ItemImage" };
            m_ItemImage.pickingMode = PickingMode.Ignore;
            m_ItemImage.AddToClassList(k_ItemIconClassName);
            rootElement.Add(m_ItemImage);

            m_SelectedImage = new VisualElement();
            m_SelectedImage.pickingMode = PickingMode.Ignore;
            m_SelectedImage.AddToClassList(k_SelectedImageClassName);
            rootElement.Add(m_SelectedImage);
        }

        void SetEventsEnabled(bool enabled)
        {
            m_EventsEnabled = enabled;
            if (enabled)
            {
                rootElement.RegisterCallback<MouseUpEvent>(OnMouseUp);
                rootElement.RegisterCallback<MouseDownEvent>(OnMouseDown);
                rootElement.RegisterCallback<MouseEnterEvent>(OnMouseEnter);
                rootElement.RegisterCallback<MouseLeaveEvent>(OnMouseMove);
            }
            else
            {
                rootElement.UnregisterCallback<MouseUpEvent>(OnMouseUp);
                rootElement.UnregisterCallback<MouseDownEvent>(OnMouseDown);
                rootElement.UnregisterCallback<MouseEnterEvent>(OnMouseEnter);
                rootElement.UnregisterCallback<MouseLeaveEvent>(OnMouseMove);
            }
        }

        void OnMouseUp(MouseUpEvent evt)
        {
            if (m_Pressed == true)
            {
                m_Pressed = false;
                onClicked?.Invoke();
            }

            onReleased?.Invoke();
        }

        void OnMouseDown(MouseDownEvent evt)
        {
            m_Pressed = true;
            m_PressedPosition = evt.mousePosition;
        }

        void OnMouseEnter(MouseEnterEvent evt)
        {
            m_Dragged = false;
        }

        void OnMouseMove(MouseLeaveEvent evt)
        {
            if (!m_Dragged && m_Pressed && Vector2.Distance(m_PressedPosition, evt.mousePosition) > k_DragRadius)
            {
                m_Pressed = false;
                m_Dragged = true;
                onDragStarted?.Invoke();
            }
        }

        public void SetSelected(bool selected)
        {
            m_Selected = selected;
            if (selected)
            {
                m_SelectedImage.AddToClassList(k_SelectedImageSelectedClassName);
            }
            else
            {
                m_SelectedImage.RemoveFromClassList(k_SelectedImageSelectedClassName);
            }
        }

        public void SetItem(Item item)
        {
            m_Item = item;
            m_RarityImage.ClearClassList();
            m_RarityImage.AddToClassList(k_RarityIconClassName);

            if (m_Item != null)
            {
                itemImageLoadHandle = Addressables.LoadAssetAsync<Texture2D>(item.name);
                itemImageLoadHandle.Completed += op =>
                {
                    if (op.Status == AsyncOperationStatus.Succeeded)
                    {
                        m_ItemImage.style.backgroundImage = op.Result;
                    }
                    else
                    {
                        Addressables.Release(itemImageLoadHandle);
                    }
                };

                switch (item.rarity)
                {
                    case Rarity.Common:
                        m_RarityImage.AddToClassList(k_RarityIconCommonClassName);
                        break;
                    case Rarity.Uncommon:
                        m_RarityImage.AddToClassList(k_RarityIconUncommonClassName);
                        break;
                    case Rarity.Rare:
                        m_RarityImage.AddToClassList(k_RarityIconRareClassName);
                        break;
                    case Rarity.Epic:
                        m_RarityImage.AddToClassList(k_RarityIconEpicClassName);
                        break;
                    case Rarity.Legendary:
                        m_RarityImage.AddToClassList(k_RarityIconLegendaryClassName);
                        break;
                }
            }
            else
            {
                m_ItemImage.style.backgroundImage = null;
                if (itemImageLoadHandle.IsValid())
                {
                    Addressables.Release(itemImageLoadHandle);
                }
            }
        }

        public VisualElement CreateDraggable()
        {
            var duplicate = new InventorySlotControl();
            duplicate.item = item;
            duplicate.selected = false;
            duplicate.eventsEnabled = false;
            return duplicate;
        }

        public void OnDestroyDraggable(VisualElement draggable)
        {
            draggable.Q<VisualElement>("ItemImage").style.backgroundImage = null;
        }
    }
}
