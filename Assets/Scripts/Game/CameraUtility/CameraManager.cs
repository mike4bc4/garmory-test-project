using System;
using System.Collections;
using System.Collections.Generic;
using Game.UserInterface;
using InputUtility;
using UnityEngine;

namespace Game.CameraUtility
{
    public class CameraManager : MonoBehaviour
    {
        static CameraManager s_Instance;

        [SerializeField] Transform m_TrackedTransform;
        [SerializeField] Camera m_Camera;

        public static Camera Camera
        {
            get => s_Instance.m_Camera;
        }

        [SerializeField] float m_Smooth;
        [SerializeField] float m_Speed;
        [SerializeField] float m_MaxRadius;
        [SerializeField] float m_InitialAlpha;
        [SerializeField] float m_InitialBeta;

        InputActions m_InputActions;
        bool m_Grabbed;
        float m_Radius;
        float m_Alpha;
        float m_Beta;
        Vector2 m_Delta;

        void Awake()
        {
            s_Instance = this;

            m_InputActions = new InputActions();
            m_InputActions.gameplay.Enable();

            m_InputActions.gameplay.cameraGrab.started += ctx =>
            {
                if (!UserInterfaceManager.IsPointerOverControl())
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    m_Grabbed = true;
                }
            };

            m_InputActions.gameplay.cameraGrab.canceled += ctx =>
            {
                if (!UserInterfaceManager.IsPointerOverControl())
                {
                    Cursor.lockState = CursorLockMode.None;
                    m_Grabbed = false;
                }
            };

            m_InputActions.gameplay.cameraLock.started += ctx =>
            {
                if (!UserInterfaceManager.IsPointerOverControl())
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    m_Grabbed = true;
                }
            };

            m_InputActions.gameplay.cameraLock.canceled += ctx =>
            {
                if (!UserInterfaceManager.IsPointerOverControl())
                {
                    Cursor.lockState = CursorLockMode.None;
                    m_Grabbed = false;
                }
            };

            m_Alpha = m_InitialAlpha;
            m_Beta = m_InitialBeta;
        }

        void Update()
        {
            UpdateCamera();
        }

        void FixedUpdate()
        {
            UpdateRadius();
        }

        void UpdateCamera()
        {
            var delta = m_Grabbed ? m_InputActions.gameplay.cameraRotate.ReadValue<Vector2>() : Vector2.zero;
            m_Delta = Vector2.Lerp(m_Delta, delta, Time.deltaTime * m_Smooth);
            m_Alpha -= m_Delta.x * 0.001f * m_Speed;

            m_Beta -= m_Delta.y * 0.001f * m_Speed;
            var clampValue = Mathf.PI * 0.4f;
            m_Beta = Mathf.Clamp(m_Beta, -clampValue, clampValue);

            float x = m_TrackedTransform.position.x + m_Radius * Mathf.Cos(m_Alpha) * Mathf.Cos(m_Beta);
            float y = m_TrackedTransform.position.y + m_Radius * Mathf.Sin(m_Beta);
            float z = m_TrackedTransform.position.z + m_Radius * Mathf.Sin(m_Alpha) * Mathf.Cos(m_Beta);

            m_Camera.transform.position = new Vector3(x, y, z);
            m_Camera.transform.LookAt(m_TrackedTransform);
        }

        void UpdateRadius()
        {
            var direction = (m_Camera.transform.position - m_TrackedTransform.position).normalized;
            if (Physics.Raycast(m_TrackedTransform.position, direction, out var hit, Mathf.Infinity))
            {
                if (hit.distance < m_MaxRadius)
                {
                    m_Radius = hit.distance;
                }
            }
            else
            {
                m_Radius = m_MaxRadius;
            }
        }
    }
}
