using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class Interactable : MonoBehaviour
    {
        public event Action onStateChange;

        State m_State;

        public State state
        {
            get => m_State;
            protected set => SetState(value);
        }

        public virtual void OnEnterRange() { }

        public virtual void OnExitRange() { }

        public virtual void OnInteract() { }

        protected void SetState(State state)
        {
            m_State = state;
            onStateChange?.Invoke();
        }
    }
}
