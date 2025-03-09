using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.UserInterface
{
    public class ObjectivePanel : Control
    {
        MainPanel m_MainPanel;

        public MainPanel mainPanel
        {
            get => m_MainPanel;
        }

        List<ObjectiveControl> m_ObjectiveControls;

        public ObjectivePanel(MainPanel mainPanel, VisualElement rootElement) : base(rootElement)
        {
            m_MainPanel = mainPanel;
            m_ObjectiveControls = new List<ObjectiveControl>();
        }

        public ObjectiveControl AddObjective()
        {
            var objective = new ObjectiveControl();
            rootElement.Add(objective);
            m_ObjectiveControls.Add(objective);
            return objective;
        }

        public void RemoveObjective(ObjectiveControl objective)
        {
            rootElement.Remove(objective);
            m_ObjectiveControls.Remove(objective);
        }
    }
}
