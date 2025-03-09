using System.Collections;
using System.Collections.Generic;
using SchedulerUtility;
using UnityEngine;
using UnityEngine.UIElements;

namespace Initialization
{
    public class UserInterface : MonoBehaviour
    {
        [SerializeField] float m_LoadingCircleFadeSpeed;
        [SerializeField] float m_LoadingCircleRotationSpeed;

        UIDocument m_UIDocument;

        Button m_PlayButton;

        VisualElement m_LoadingCircle;

        IVisualElementScheduledItem m_ShowLoadingCircleAnimation;
        IVisualElementScheduledItem m_RotateLoadingCircleAnimation;

        float m_LoadingCircleOpacity;
        float m_LoadingCircleRotation;

        void Awake()
        {
            m_UIDocument = GetComponent<UIDocument>();
            m_PlayButton = m_UIDocument.rootVisualElement.Q<Button>("PlayButton");
            m_PlayButton.clicked += OnPlayButtonClicked;

            m_LoadingCircle = m_UIDocument.rootVisualElement.Q("LoadingCircle");
            m_LoadingCircle.style.opacity = 0f;
        }

        void OnPlayButtonClicked()
        {
            m_PlayButton.SetEnabled(false);
            m_ShowLoadingCircleAnimation = m_LoadingCircle.schedule.Execute(() =>
            {
                m_LoadingCircleOpacity = Mathf.Min(1f, m_LoadingCircleOpacity + m_LoadingCircleFadeSpeed * Time.deltaTime);
                m_LoadingCircle.style.opacity = m_LoadingCircleOpacity;

                if (m_LoadingCircleOpacity == 1f)
                {
                    m_ShowLoadingCircleAnimation.Pause();
                    m_ShowLoadingCircleAnimation = null;
                }
            }).Every(0);

            m_RotateLoadingCircleAnimation = m_LoadingCircle.schedule.Execute(() =>
            {
                m_LoadingCircleRotation += m_LoadingCircleRotationSpeed * Time.deltaTime;
                m_LoadingCircle.transform.rotation = Quaternion.Euler(0f, 0f, m_LoadingCircleRotation);
            }).Every(0);

            m_UIDocument.rootVisualElement.schedule.Execute(() =>
            {
                Loader.Instance.LoadMainScene();
            }).StartingIn(1000);
        }
    }
}
