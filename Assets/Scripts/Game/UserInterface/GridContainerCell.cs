using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UserInterface
{
    public class GridContainerCell : SelfContainedControl
    {
        const string k_ClassName = "grid-container__row__cell";
        const string k_LastClassName = k_ClassName + "--last";

        bool m_IsLast;

        public bool isLast
        {
            get => m_IsLast;
            set => SetIsLast(value);
        }

        public GridContainerCell()
        {
            rootElement.AddToClassList(k_ClassName);
        }

        public void SetIsLast(bool isLast)
        {
            m_IsLast = isLast;
            if (m_IsLast)
            {
                rootElement.AddToClassList(k_LastClassName);
            }
            else
            {
                rootElement.RemoveFromClassList(k_LastClassName);
            }
        }
    }
}
