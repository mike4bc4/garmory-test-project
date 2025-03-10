using System;
using System.Collections;
using System.Collections.Generic;
using SchedulerUtility;
using UnityEngine;

namespace Game.CharacterUtility
{
    public class AnimationSystem
    {
        public enum State
        {
            None,
            Idle,
            Run,
            RunBack,
            LeftTurn,
            RightTurn
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] Animator m_Animator;

            public Animator animator
            {
                get => m_Animator;
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

        protected Animator animator
        {
            get => m_Settings.animator;
        }

        IScheduledItem m_CombatLayerWeightAnimation;
        State m_State;

        public AnimationSystem(Character character, Settings settings)
        {
            m_Character = character;
            m_Settings = settings;
        }

        public void Run()
        {
            if (m_State != State.Run)
            {
                m_State = State.Run;
                animator.CrossFadeInFixedTime("Base.Run", 0.25f);
            }
        }

        public void RunBack()
        {
            if (m_State != State.RunBack)
            {
                m_State = State.RunBack;
                animator.CrossFadeInFixedTime("Base.RunBack", 0.25f);
            }
        }

        public void Idle()
        {
            if (m_State != State.Idle)
            {
                m_State = State.Idle;
                animator.CrossFadeInFixedTime("Base.Idle", 0.25f);
            }
        }

        public void LeftTurn()
        {
            if (m_State != State.LeftTurn)
            {
                m_State = State.LeftTurn;
                animator.CrossFadeInFixedTime("Base.LeftTurn", 0.25f);
            }
        }

        public void RightTurn()
        {
            if (m_State != State.RightTurn)
            {
                m_State = State.RightTurn;
                animator.CrossFadeInFixedTime("Base.RightTurn", 0.25f);
            }
        }

        AnimationClip GetAnimationClip(string name)
        {
            foreach (var clip in animator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == name)
                {
                    return clip;
                }
            }

            return null;
        }

        public void Attack(float duration = 1f)
        {
            var animationClip = GetAnimationClip("Attack2");
            var speedMultiplier = animationClip.length / duration;
            animator.SetFloat("AttackSpeed", speedMultiplier);

            m_CombatLayerWeightAnimation?.Cancel();

            if (m_State == State.Idle)
            {
                animator.CrossFadeInFixedTime("Base.Attack2", 0.25f);
            }
            else
            {
                var combatLayerWeight = 0f;
                var timer = 0f;
                m_CombatLayerWeightAnimation = Scheduler.Every(0, () =>
                {
                    timer += Time.deltaTime;
                    if (timer <= duration * 0.25f)
                    {
                        combatLayerWeight = Mathf.Clamp01(timer / (duration * 0.25f));
                    }
                    else if (timer >= duration * 0.75f)
                    {
                        combatLayerWeight = Mathf.Clamp01(1f - (timer - duration * 0.75f) / (duration * 0.25f));
                    }
                    else
                    {
                        combatLayerWeight = 1f;
                    }

                    animator.SetLayerWeight(1, combatLayerWeight);

                    if (timer >= duration)
                    {
                        m_CombatLayerWeightAnimation.Cancel();
                    }
                });

                animator.Play("Combat.Attack2", 1, 0f);
            }
        }
    }
}
