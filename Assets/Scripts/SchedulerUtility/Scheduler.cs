using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SchedulerUtility
{
    public interface IScheduledItem
    {
        public void Cancel();
    }

    public class Scheduler : MonoBehaviour
    {
        class ScheduledItem : IScheduledItem
        {
            public Action action;

            public void Cancel()
            {
                OnUpdate -= action;
            }
        }

        public static event Action OnUpdate;

        static Scheduler s_Instance;

        [RuntimeInitializeOnLoadMethod]
        static void InitializeOnLoad()
        {
            var gameObject = new GameObject("Scheduler");
            s_Instance = gameObject.AddComponent<Scheduler>();
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            OnUpdate?.Invoke();
        }

        public static IScheduledItem WaitUntil(Func<bool> predicate, Action callback)
        {
            if (predicate())
            {
                callback?.Invoke();
                return null;
            }

            Action action = null;
            OnUpdate += action = () =>
            {
                if (predicate())
                {
                    callback?.Invoke();
                    OnUpdate -= action;
                }
            };

            return new ScheduledItem() { action = action };
        }
    }
}
