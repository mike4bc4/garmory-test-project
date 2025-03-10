using System;
using System.Collections;
using System.Collections.Generic;
using Game.CameraUtility;
using InputUtility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.CharacterUtility
{
    public class MovementSystem
    {
        [Serializable]
        public class Settings
        {
            [SerializeField] float m_MovementSpeed;

            public float movementSpeed
            {
                get => m_MovementSpeed;
            }

            [SerializeField] float m_JumpHeight;

            public float jumpHeight
            {
                get => m_JumpHeight;
            }

            [SerializeField] float m_Gravity;

            public float gravity
            {
                get => m_Gravity;
            }

            [SerializeField] float m_RotationSpeed;

            public float rotationSpeed
            {
                get => m_RotationSpeed;
            }
        }

        Settings m_Settings;

        public Settings settings
        {
            get => m_Settings;
        }

        Character m_Character;

        public Character character
        {
            get => m_Character;
        }

        InputAction m_MoveAction;
        bool m_CameraLocked;

        public MovementSystem(Character character, Settings settings)
        {
            m_Character = character;
            m_Settings = settings;

            m_MoveAction = InputManager.InputActions.gameplay.move;

            InputManager.InputActions.gameplay.cameraLock.started += ctx =>
            {
                m_CameraLocked = true;
            };

            InputManager.InputActions.gameplay.cameraLock.canceled += ctx =>
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
                    m_Character.transform.position += m_Character.transform.forward * settings.movementSpeed * Time.deltaTime;
                    character.animationSystem.Run();
                }
                else
                {
                    character.animationSystem.Idle();
                }
            }
            else
            {
                var moveVector = m_MoveAction.ReadValue<Vector2>();
                moveVector.x = Mathf.Round(moveVector.x);
                moveVector.y = Mathf.Round(moveVector.y);
                m_Character.transform.Rotate(Vector3.up, settings.rotationSpeed * moveVector.x * Time.deltaTime);
                m_Character.transform.position += m_Character.transform.forward * settings.movementSpeed * moveVector.y * Time.deltaTime;

                if (moveVector.y > 0)
                {
                    character.animationSystem.Run();
                }
                else if (moveVector.y < 0)
                {
                    character.animationSystem.RunBack();
                }
                else if (moveVector.x > 0)
                {
                    character.animationSystem.RightTurn();
                }
                else if (moveVector.x < 0)
                {
                    character.animationSystem.LeftTurn();
                }
                else
                {
                    character.animationSystem.Idle();
                }
            }
        }
    }
}
