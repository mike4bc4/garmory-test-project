using System.Collections;
using System.Collections.Generic;
using Game.UserInterface;
using UnityEngine;

namespace Game.Objectives
{
    public class Objective
    {
        string m_Header;

        public string header
        {
            get => m_Header;
            set => SetHeader(value);
        }

        string m_Description;

        public string description
        {
            get => m_Description;
            set => SetDescription(value);
        }

        ObjectiveControl m_Control;

        public Objective()
        {
            m_Control = UserInterfaceManager.MainPanel.objectivePanel.AddObjective();
        }

        public void SetHeader(string header)
        {
            m_Header = header;
            m_Control.headerText = header;
        }

        public void SetDescription(string description)
        {
            m_Description = description;
            m_Control.descriptionText = description;
        }
    }
}