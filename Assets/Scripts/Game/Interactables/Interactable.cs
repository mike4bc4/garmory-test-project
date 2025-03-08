using System;
using System.Collections;
using System.Collections.Generic;
using Game.CharacterUtility;
using UnityEngine;

namespace Game.Interactables
{
    public class Interactable : MonoBehaviour
    {
        public virtual void OnEnterRange(Character character) { }

        public virtual void OnExitRange(Character character) { }

        public virtual void OnInteract(Character character) { }
    }
}
