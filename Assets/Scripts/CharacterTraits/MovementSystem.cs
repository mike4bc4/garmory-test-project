using System;
using System.Collections;
using System.Collections.Generic;
using CameraUtility;
using InputUtilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CharacterTraits
{
    public class MovementSystem
    {
        Character m_Character;

        public Character character
        {
            get => m_Character;
        }

        InputActions m_InputActions;
        InputAction m_MoveAction;
        bool m_CameraLocked;

        public MovementSystem(Character character)
        {
            m_Character = character;

            m_InputActions = new InputActions();
            m_InputActions.gameplay.Enable();
            m_MoveAction = m_InputActions.gameplay.move;

            m_InputActions.gameplay.cameraLock.started += ctx =>
            {
                m_CameraLocked = true;
            };

            m_InputActions.gameplay.cameraLock.canceled += ctx =>
            {
                m_CameraLocked = false;
            };
        }

        public void Update()
        {
            if (m_CameraLocked)
            {
                var moveVector = m_MoveAction.ReadValue<Vector2>();
                if (moveVector != Vector2.zero)
                {
                    var forward = CameraManager.Camera.transform.forward;
                    m_Character.transform.forward = new Vector3(forward.x, 0f, forward.z).normalized;
                    m_Character.transform.forward = ((m_Character.transform.forward * moveVector.y + m_Character.transform.right * moveVector.x) / 2f).normalized;
                    m_Character.transform.position += m_Character.transform.forward * character.speed * Time.deltaTime;
                }
            }
            else
            {
                var moveVector = m_MoveAction.ReadValue<Vector2>();
                m_Character.transform.Rotate(Vector3.up, character.rotationSpeed * moveVector.x * Time.deltaTime);
                m_Character.transform.position += m_Character.transform.forward * character.speed * moveVector.y * Time.deltaTime;
            }
        }
    }
}
