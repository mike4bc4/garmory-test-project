using System.Collections;
using System.Collections.Generic;
using CharacterFeatures;
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

        Item m_Item;

        public Item item
        {
            get => m_Item;
            set => SetItem(value);
        }

        Item m_CompareItem;

        public Item compareItem
        {
            get => m_CompareItem;
            set => SetCompareItem(value);
        }

        public ItemDescriptionPanel(VisualElement rootElement) : base(rootElement)
        {
            m_NameLabel = rootElement.Q<Label>("NameLabel");
            m_ScrollView = rootElement.Q<ScrollView>();
            m_ItemInfoContainer = rootElement.Q<VisualElement>("ItemInfoContainer");
            m_ImageElement = rootElement.Q<VisualElement>("ImageElement");
            m_StatsContainer = rootElement.Q<VisualElement>("StatsContainer");
            SetItem(null);
            SetCompareItem(null);
        }

        void RefreshItemAttributes()
        {
            m_StatsContainer.Clear();
            if (item == null)
            {
                return;
            }

            for (int i = 0; i < item.attributes.Count; i++)
            {
                CharacterAttribute attribute = item.attributes[i];
                CharacterAttribute compareAttribute = compareItem?.attributes[i];
                if (!attribute.isEmpty)
                {
                    m_StatsContainer.Add(new AttributeControl()
                    {
                        attribute = attribute,
                        compareAttribute = compareAttribute,
                        valueChangeVisible = item != compareItem,
                    });
                }
            }
        }

        public void SetCompareItem(Item item)
        {
            m_CompareItem = item;
            RefreshItemAttributes();
        }

        public void SetItem(Item item)
        {
            m_Item = item;
            m_ScrollView.scrollOffset = Vector2.zero;
            if (item != null)
            {
                SetItemInfoContainerVisible(true);
                m_NameLabel.text = item.name;
                itemImageLoadHandle = Addressables.LoadAssetAsync<Texture2D>(item.name);
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
            }
            else
            {
                SetItemInfoContainerVisible(false);
                m_NameLabel.text = "Nothing Selected";
                if (itemImageLoadHandle.IsValid())
                {
                    Addressables.Release(itemImageLoadHandle);
                }
            }

            RefreshItemAttributes();
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
