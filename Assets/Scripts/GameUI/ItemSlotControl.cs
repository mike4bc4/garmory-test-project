using System;
using System.Collections;
using System.Collections.Generic;
using EquipmentItems;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

namespace GameUI
{
    public class ItemSlotControl : SelfContainedControl, IDraggableControl
    {
        const string k_ClassName = "item-slot";
        const string k_BackgroundClassName = k_ClassName + "__background";
        const string k_BackgroundHiddenCategoryClassName = k_ClassName + "--category-hidden";
        const string k_RarityClassName = k_ClassName + "__rarity";
        const string k_ItemClassName = k_ClassName + "__item";
        const string k_SelectedClassName = k_ClassName + "__selected";
        const float k_DragRadius = 10f;

        public event Action onItemChanged;
        public event Action onReleased;
        public event Action onClicked;
        public event Action onDragStarted;

        VisualElement m_BackgroundImageElement;
        VisualElement m_RarityImageElement;
        VisualElement m_ItemImageElement;
        VisualElement m_SelectedImageElement;
        AsyncOperationHandle<Texture2D> m_ItemImageLoadHandle;
        Vector2 m_PressedPosition;
        bool m_Dragged;
        bool m_Pressed;

        public Category m_Category;

        public Category category
        {
            get => m_Category;
            set => SetCategory(value);
        }

        Item m_Item;

        public Item item
        {
            get => m_Item;
            set => SetItem(value);
        }

        bool m_Selected;

        public bool selected
        {
            get => m_Selected;
            set => SetSelected(value);
        }

        bool m_EventsEnabled;

        public bool eventsEnabled
        {
            get => m_EventsEnabled;
            set => SetEventsEnabled(value);
        }

        bool m_SuppressItemChangedEvent;

        public bool suppressItemChangedEvent
        {
            get => m_SuppressItemChangedEvent;
            set => m_SuppressItemChangedEvent = value;
        }

        public ItemSlotControl()
        {
            rootElement.AddToClassList(k_ClassName);
            rootElement.name = "EquipmentSlot";

            m_BackgroundImageElement = new VisualElement() { name = "BackgroundImage", pickingMode = PickingMode.Ignore };
            m_BackgroundImageElement.AddToClassList(k_BackgroundClassName);
            rootElement.Add(m_BackgroundImageElement);

            m_RarityImageElement = new VisualElement() { name = "RarityImage", pickingMode = PickingMode.Ignore };
            m_RarityImageElement.AddToClassList(k_RarityClassName);
            rootElement.Add(m_RarityImageElement);

            m_ItemImageElement = new VisualElement() { name = "ItemImage", pickingMode = PickingMode.Ignore };
            m_ItemImageElement.AddToClassList(k_ItemClassName);
            rootElement.Add(m_ItemImageElement);

            m_SelectedImageElement = new VisualElement() { name = "SelectedImage", pickingMode = PickingMode.Ignore };
            m_SelectedImageElement.AddToClassList(k_SelectedClassName);
            rootElement.Add(m_SelectedImageElement);

            category = Category.None;
            item = null;
            eventsEnabled = true;
            selected = false;
            suppressItemChangedEvent = false;
        }

        void SetEventsEnabled(bool enabled)
        {
            m_EventsEnabled = enabled;
            if (enabled)
            {
                rootElement.RegisterCallback<MouseUpEvent>(OnMouseUp);
                rootElement.RegisterCallback<MouseDownEvent>(OnMouseDown);
                rootElement.RegisterCallback<MouseEnterEvent>(OnMouseEnter);
                rootElement.RegisterCallback<MouseMoveEvent>(OnMouseMove);
            }
            else
            {
                rootElement.UnregisterCallback<MouseUpEvent>(OnMouseUp);
                rootElement.UnregisterCallback<MouseDownEvent>(OnMouseDown);
                rootElement.UnregisterCallback<MouseEnterEvent>(OnMouseEnter);
                rootElement.UnregisterCallback<MouseMoveEvent>(OnMouseMove);
            }
        }

        void OnMouseUp(MouseUpEvent evt)
        {
            m_Dragged = false;
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

        void OnMouseMove(MouseMoveEvent evt)
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
            m_SelectedImageElement.style.display = m_Selected ? DisplayStyle.Flex : DisplayStyle.None;
        }

        public void SetCategory(Category category)
        {
            m_Category = category;
            m_BackgroundImageElement.ClearClassList();
            m_BackgroundImageElement.AddToClassList(k_BackgroundClassName);
            if (m_Category != Category.None)
            {
                m_BackgroundImageElement.AddToClassList(k_BackgroundClassName + "--" + m_Category.ToString().ToLower());
            }
        }

        public void SetItem(Item item)
        {
            m_Item = item;
            m_RarityImageElement.ClearClassList();
            m_RarityImageElement.AddToClassList(k_RarityClassName);
            if (item != null)
            {
                m_RarityImageElement.AddToClassList(k_RarityClassName + "--" + item.rarity.ToString().ToLower());
                m_BackgroundImageElement.AddToClassList(k_BackgroundHiddenCategoryClassName);
                m_ItemImageLoadHandle = Addressables.LoadAssetAsync<Texture2D>(item.name);
                m_ItemImageLoadHandle.Completed += op =>
                {
                    if (op.Status == AsyncOperationStatus.Succeeded)
                    {
                        m_ItemImageElement.style.backgroundImage = op.Result;
                    }
                    else
                    {
                        Addressables.Release(m_ItemImageLoadHandle);
                    }
                };
            }
            else
            {
                m_BackgroundImageElement.RemoveFromClassList(k_BackgroundHiddenCategoryClassName);
                m_ItemImageElement.style.backgroundImage = null;
                if (m_ItemImageLoadHandle.IsValid())
                {
                    Addressables.Release(m_ItemImageLoadHandle);
                }
            }

            if (!m_SuppressItemChangedEvent)
            {
                onItemChanged?.Invoke();
            }
        }

        public void OnDestroy()
        {
            m_ItemImageElement.style.backgroundImage = null;
            if (m_ItemImageLoadHandle.IsValid())
            {
                Addressables.Release(m_ItemImageLoadHandle);
            }
        }

        public VisualElement CreateDraggable()
        {
            var duplicate = new ItemSlotControl()
            {
                item = item,
                selected = false,
                eventsEnabled = false
            };

            var elements = duplicate.rootElement.Query<VisualElement>().ToList();
            foreach (var element in elements)
            {
                element.pickingMode = PickingMode.Ignore;
            }

            return duplicate;
        }

        public void OnDestroyDraggable(VisualElement draggable)
        {
            draggable.Q<VisualElement>("ItemImage").style.backgroundImage = null;
        }
    }
}
