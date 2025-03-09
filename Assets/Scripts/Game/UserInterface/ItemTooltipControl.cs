using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.EquipmentItems;
using SchedulerUtility;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

namespace Game.UserInterface
{
    public class ItemTooltipControl : Control
    {
        const string k_ClassName = "item-tooltip";
        const string k_RarityClassName = k_ClassName + "__rarity";

        Label m_NameLabel;
        VisualElement m_RarityImage;
        VisualElement m_ItemImage;
        VisualElement m_AttributeContainer;

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

        public ItemTooltipPanel itemTooltipPanel
        {
            get => UserInterfaceManager.Instance.mainPanel.itemTooltipPanel;
        }

        AsyncOperationHandle<Texture2D> m_ItemImageLoadHandle;

        public ItemTooltipControl()
        {
            var template = UserInterfaceManager.Instance.itemTooltipControl.Instantiate();
            rootElement = template.hierarchy.Children().First();

            foreach (var element in rootElement.Query<VisualElement>().ToList())
            {
                element.pickingMode = PickingMode.Ignore;
            }

            m_NameLabel = rootElement.Q<Label>("NameLabel");
            m_RarityImage = rootElement.Q("RarityImage");
            m_ItemImage = rootElement.Q("ItemImage");
            m_AttributeContainer = rootElement.Q("AttributeContainer");

            SetItemInternal(null);
            SetCompareItemInternal(null);
        }

        void TrackPointer()
        {
            var r1 = itemTooltipPanel.rootElement.worldBound;
            var r2 = rootElement.worldBound;
            float xMax = r1.width - r2.width;
            float yMax = r1.height - r2.height;

            var pointerPosition = UserInterfaceManager.PointerPosition;
            rootElement.style.left = Mathf.Min(pointerPosition.x, xMax);
            rootElement.style.top = Mathf.Min(pointerPosition.y, yMax);
            rootElement.style.right = StyleKeyword.Auto;
            rootElement.style.bottom = StyleKeyword.Auto;
        }

        public void SetItem(Item item)
        {
            if (item != m_Item)
            {
                SetItemInternal(item);
            }
        }

        void SetItemInternal(Item item)
        {
            m_Item = item;
            if (item != null)
            {
                Scheduler.OnUpdate += TrackPointer;

                m_NameLabel.text = item.displayName;
                m_RarityImage.AddToClassList(k_RarityClassName + "--" + item.rarity.ToString().ToLower());
                m_ItemImageLoadHandle = Addressables.LoadAssetAsync<Texture2D>(item.name);
                m_ItemImageLoadHandle.Completed += op =>
                {
                    if (op.Status == AsyncOperationStatus.Succeeded)
                    {
                        m_ItemImage.style.backgroundImage = op.Result;
                    }
                    else
                    {
                        Addressables.Release(m_ItemImageLoadHandle);
                    }
                };
            }
            else
            {
                Scheduler.OnUpdate -= TrackPointer;

                m_NameLabel.text = "None";
                m_RarityImage.ClearClassList();
                m_ItemImage.style.backgroundImage = null;
                if (m_ItemImageLoadHandle.IsValid())
                {
                    Addressables.Release(m_ItemImageLoadHandle);
                }
            }

            // Display next frame to avoid showing tooltip at panel root position.
            Scheduler.NextFrame(() =>
            {
                rootElement.style.display = item != null ? DisplayStyle.Flex : DisplayStyle.None;
            });

            RefreshItemAttributes();
        }

        public void SetCompareItem(Item item)
        {
            if (item != m_CompareItem)
            {
                SetCompareItemInternal(item);
            }
        }

        void SetCompareItemInternal(Item item)
        {
            m_CompareItem = item;
            RefreshItemAttributes();
        }

        void RefreshItemAttributes()
        {
            m_AttributeContainer.Clear();
            if (item == null)
            {
                return;
            }

            for (int i = 0; i < item.attributes.Count; i++)
            {
                Attribute attribute = item.attributes[i];
                Attribute compareAttribute = compareItem?.attributes[i];
                if (!attribute.isEmpty)
                {
                    m_AttributeContainer.Add(new AttributeControl()
                    {
                        attribute = attribute,
                        compareAttribute = compareAttribute,
                        valueChangeVisible = item != compareItem,
                    });
                }
            }
        }
    }
}
