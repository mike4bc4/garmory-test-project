using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUI
{
    public class GridContainerCell : SelfContainedControl
    {
        const string k_ClassName = "grid-container__row__cell";

        public GridContainerCell()
        {
            rootElement.AddToClassList(k_ClassName);
        }
    }
}
