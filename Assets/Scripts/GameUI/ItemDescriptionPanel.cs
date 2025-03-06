using System.Collections;
using System.Collections.Generic;
using EquipmentItems;
using ResourcesUtility;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

namespace GameUI
{
    public class ItemDescriptionPanel : Control
    {
        Label m_NameLabel;
        ScrollView m_ScrollView;
        VisualElement m_ItemInfoContainer;
        VisualElement m_ImageElement;
        VisualElement m_StatsContainer;
        AsyncOperationHandle<Texture2D> itemImageLoadHandle;

        InventorySlotControl m_SlotControl;

        public InventorySlotControl slotControl
        {
            get => m_SlotControl;
            set => SetSlotControl(value);
        }

        public ItemDescriptionPanel(VisualElement rootElement) : base(rootElement)
        {
            m_NameLabel = rootElement.Q<Label>("NameLabel");
            m_ScrollView = rootElement.Q<ScrollView>();
            m_ItemInfoContainer = rootElement.Q<VisualElement>("ItemInfoContainer");
            m_ImageElement = rootElement.Q<VisualElement>("ImageElement");
            m_StatsContainer = rootElement.Q<VisualElement>("StatsContainer");
            SetSlotControl(null);
        }

        public void SetSlotControl(InventorySlotControl slotControl)
        {
            m_SlotControl = slotControl;
            m_ScrollView.scrollOffset = Vector2.zero;
            if (m_SlotControl?.item != null)
            {
                SetItemInfoContainerVisible(true);
                m_NameLabel.text = m_SlotControl.item.name;
                itemImageLoadHandle = Addressables.LoadAssetAsync<Texture2D>(m_SlotControl.item.name);
                itemImageLoadHandle.Completed += op =>
                {
                    if (op.Status == AsyncOperationStatus.Succeeded)
                    {
                        m_ImageElement.style.backgroundImage = op.Result;
                    }
                    else
                    {
                        Addressables.Release(itemImageLoadHandle);
                    }
                };

                m_StatsContainer.Clear();
                foreach (var attribute in m_SlotControl.item.attributes)
                {
                    if (!attribute.isEmpty)
                    {
                        m_StatsContainer.Add(new AttributeControl() { attribute = attribute });
                    }
                }
            }
            else
            {
                SetItemInfoContainerVisible(false);
                m_NameLabel.text = "Nothing Selected";
                if (itemImageLoadHandle.IsValid())
                {
                    Addressables.Release(itemImageLoadHandle);
                }

                m_StatsContainer.Clear();
            }
        }

        void SetItemInfoContainerVisible(bool visible)
        {
            if (visible)
            {
                m_ItemInfoContainer.style.display = DisplayStyle.Flex;
            }
            else
            {
                m_ItemInfoContainer.style.display = DisplayStyle.None;
            }
        }
    }
}
