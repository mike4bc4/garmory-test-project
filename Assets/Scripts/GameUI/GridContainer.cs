using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameUI
{
    public class GridContainer : Control
    {
        List<GridContainerRow> m_Rows;

        public List<GridContainerCell> cells
        {
            get
            {
                List<GridContainerCell> cells = new List<GridContainerCell>();
                foreach (var row in m_Rows)
                {
                    cells.AddRange(row.cells);
                }

                return cells;
            }
        }

        public GridContainer(VisualElement rootElement) : base(rootElement)
        {
            m_Rows = new List<GridContainerRow>();
        }

        public void SetSize(int size, int rowSize)
        {
            rootElement.Clear();
            m_Rows.Clear();

            int fullRowsCount = size / rowSize;
            for (int i = 0; i < fullRowsCount; i++)
            {
                var row = new GridContainerRow(rowSize);
                m_Rows.Add(row);
                rootElement.Add(row);
            }

            int lastRowSize = size - fullRowsCount * rowSize;
            if (lastRowSize > 0)
            {
                var row = new GridContainerRow(lastRowSize);
                m_Rows.Add(row);
                rootElement.Add(row);
            }
        }
    }
}
