using System;
using System.Collections;
using System.Collections.Generic;
using InputUtilities;
using Interactables;
using SchedulerUtility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CharacterTraits
{
    public class InteractableTracker
    {
        [Serializable]
        public class Settings
        {
            [SerializeField] float m_Range;

            public float range
            {
                get => m_Range;
            }

            [SerializeField] float m_Interval;

            public float interval
            {
                get => m_Interval;
            }
        }

        Character m_Character;

        public Character character
        {
            get => m_Character;
        }

        Settings m_Settings;

        public Settings settings
        {
            get => m_Settings;
        }

        bool m_Enabled;

        public bool enabled
        {
            get => m_Enabled;
            set => SetEnabled(value);
        }

        Interactable m_CurrentInteractable;

        IScheduledItem m_TrackScheduledItem;

        InputActions m_InputActions;

        public InteractableTracker(Character character, Settings settings)
        {
            m_Character = character;
            m_Settings = settings;

            m_InputActions = new InputActions();
            m_InputActions.Enable();
            m_InputActions.gameplay.interact.performed += ctx =>
            {
                if (m_CurrentInteractable != null)
                {
                    m_CurrentInteractable.OnInteract();
                }
            };

            enabled = true;
        }

        public void SetEnabled(bool enabled)
        {
            m_Enabled = enabled;
            if (m_Enabled)
            {
                if (m_TrackScheduledItem == null)
                {
                    m_TrackScheduledItem = Scheduler.Every(settings.interval, Track);
                }
            }
            else
            {
                if (m_TrackScheduledItem != null)
                {
                    m_TrackScheduledItem.Cancel();
                    m_TrackScheduledItem = null;
                }
            }
        }

        void Track()
        {
            Interactable nearestInteractable = null;
            var distanceToNearestInteractable = Mathf.Infinity;
            foreach (var interactable in InteractableManager.Interactables)
            {
                var distance = Vector3.Distance(interactable.transform.position, character.transform.position);
                if (distance < settings.range && distance < distanceToNearestInteractable)
                {
                    distanceToNearestInteractable = distance;
                    nearestInteractable = interactable;
                }
            }

            if (nearestInteractable != m_CurrentInteractable)
            {
                if (m_CurrentInteractable != null)
                {
                    m_CurrentInteractable.OnExitRange();
                }

                if (nearestInteractable != null)
                {
                    nearestInteractable.OnEnterRange();
                }

                m_CurrentInteractable = nearestInteractable;
            }
        }
    }
}
