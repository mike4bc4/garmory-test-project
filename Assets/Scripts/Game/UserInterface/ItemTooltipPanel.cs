using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.UserInterface
{
    public class ItemTooltipPanel : Control
    {
        MainPanel m_MainPanel;

        public MainPanel mainPanel
        {
            get => m_MainPanel;
        }

        ItemTooltipControl m_Tooltip;

        public ItemTooltipControl tooltip
        {
            get => m_Tooltip;
        }

        public ItemTooltipPanel(MainPanel mainPanel, VisualElement rootElement) : base(rootElement)
        {
            m_MainPanel = mainPanel;
            m_Tooltip = new ItemTooltipControl();
            rootElement.Add(m_Tooltip);
        }
    }
}
