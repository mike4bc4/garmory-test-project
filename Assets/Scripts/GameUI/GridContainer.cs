using System;
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

        public GridContainerCell this[int index]
        {
            get
            {
                if (index < 0 || cells.Count <= index)
                {
                    throw new IndexOutOfRangeException();
                }

                return cells[index];
            }
        }

        public GridContainer(VisualElement rootElement) : base(rootElement)
        {
            m_Rows = new List<GridContainerRow>();
        }

        public void Clear()
        {
            rootElement.Clear();
            m_Rows.Clear();
        }

        public void SetSize(int size, int rowSize)
        {
            Clear();
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

        public void SetCellsVisible(bool visible, params int[] indexes)
        {
            var cells = this.cells;
            var visibility = visible ? Visibility.Visible : Visibility.Hidden;
            foreach (var index in indexes)
            {
                if (index >= 0 && cells.Count > index)
                {
                    cells[index].rootElement.style.visibility = visibility;
                }
            }
        }
    }
}
