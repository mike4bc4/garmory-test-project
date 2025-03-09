using System.Collections;
using System.Collections.Generic;
using SchedulerUtility;
using UnityEngine;

namespace Game.VisualEffects
{
    public class Slash : MonoBehaviour
    {
        [SerializeField] float m_Speed;

        float m_Offset;
        Material m_Material;
        bool m_IsPlaying;

        public bool isPlaying
        {
            get => m_IsPlaying;
        }

        void Start()
        {
            m_Material = GetComponent<MeshRenderer>().material;
            m_Offset = -1;

            m_Material.SetFloat("_Offset", m_Offset);
        }

        public void Play()
        {
            if (m_IsPlaying)
            {
                return;
            }

            Scheduler.OnUpdate += Animate;
            m_IsPlaying = true;
            m_Offset = -1;

            m_Material.SetFloat("_Offset", m_Offset);
        }

        void Animate()
        {
            m_Offset += Time.deltaTime * m_Speed;
            if (m_Offset >= 1f)
            {
                Scheduler.OnUpdate -= Animate;
                m_IsPlaying = false;
                m_Offset = -1;
            }

            m_Material.SetFloat("_Offset", m_Offset);
        }
    }
}
