using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ResourcesUtility
{
    public static class ItemIconsManager
    {
        private class OperationHandleEntry
        {
            public string address;
            public AsyncOperationHandle<Texture2D> handle;
        }

        static List<OperationHandleEntry> s_OperationHandles;

        static List<OperationHandleEntry> OperationHandles
        {
            get
            {
                if (s_OperationHandles == null)
                {
                    s_OperationHandles = new List<OperationHandleEntry>();
                }

                return s_OperationHandles;
            }
        }

        public static void LoadItemIcon(string itemName, Action<Texture2D> onFinished, Action onFailed = null)
        {
            var operationHandle = Addressables.LoadAssetAsync<Texture2D>(itemName);   
            operationHandle.Completed += op =>
            {
                if (op.Status == AsyncOperationStatus.Succeeded)
                {
                    OperationHandles.Add(new OperationHandleEntry() { address = itemName, handle = operationHandle });
                    onFinished?.Invoke(op.Result);
                }
                else
                {
                    onFailed?.Invoke();
                    Addressables.Release(operationHandle);
                }
            };
        }

        public static void ReleaseItemIcon(string itemName)
        {
            for (int i = 0; i < OperationHandles.Count; i++)
            {
                OperationHandleEntry operationHandle = OperationHandles[i];
                if (operationHandle.address == itemName)
                {
                    Debug.Log("A");
                    Addressables.Release(operationHandle.handle);
                    OperationHandles.RemoveAt(i);
                    break;
                }
            }
        }

        public static void ReleaseAllItemIcons()
        {
            foreach (var operationHandle in OperationHandles)
            {
                Addressables.Release(operationHandle.handle);
            }

            OperationHandles.Clear();
        }
    }
}
