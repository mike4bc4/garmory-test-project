using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameUI
{
    public interface IDraggableControl
    {
        public VisualElement CreateDraggable();

        public void OnDestroyDraggable(VisualElement draggable);
    }
}
