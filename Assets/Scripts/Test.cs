using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GameServerUtility;
using Newtonsoft.Json;
using SchedulerUtility;
using UnityEngine;
using UnityEngine.AddressableAssets;


public class Test : MonoBehaviour
{
    void Start()
    {
        // var handle = Addressables.LoadAssetAsync<Texture2D>("WolfArmor");
        // handle.Completed += op =>
        // {
        //     Debug.Log(op.Result);
        //     Addressables.Release(handle);
        // };

        // var gameServerMock = new GameServerMock();
        // Task<string> task = gameServerMock.GetItemsAsync();
        // Scheduler.WaitUntil(() => task.IsCompleted, () =>
        // {
        //     Debug.Log("Completed");
        // });

        // task.ContinueWith(antecedent =>
        // {
        //     // var itemsWrapper = JsonConvert.DeserializeObject<ServerResponseItemsContainerWrapper>(antecedent.Result);
        //     // Debug.Log(itemsWrapper.Items.Count);
        //     Debug.Log("TaskFinished");
        // });
    }
}
