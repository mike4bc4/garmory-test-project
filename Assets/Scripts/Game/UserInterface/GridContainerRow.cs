using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.UserInterface
{
    public class GridContainerRow : SelfContainedControl
    {
        const string k_ClassName = "grid-container__row";

        List<GridContainerCell> m_Cells;

        public IReadOnlyList<GridContainerCell> cells
        {
            get => m_Cells.AsReadOnly();
        }

        public GridContainerRow(int size)
        {
            rootElement.AddToClassList(k_ClassName);
            m_Cells = new List<GridContainerCell>();

            for (int i = 0; i < size; i++)
            {
                var cell = new GridContainerCell();
                m_Cells.Add(cell);
                rootElement.Add(cell);
            }

            if (m_Cells.Count > 0)
            {
                m_Cells.Last().isLast = true;
            }
        }
    }
}
