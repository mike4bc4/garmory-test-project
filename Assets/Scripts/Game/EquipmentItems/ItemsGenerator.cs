using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Newtonsoft.Json;
using SchedulerUtility;
using UnityEngine;

namespace Game.EquipmentItems
{
    public static class ItemsGenerator
    {
        static GameServerMock s_Server;

        static GameServerMock Server
        {
            get
            {
                if (s_Server == null)
                {
                    s_Server = new GameServerMock();
                }

                return s_Server;
            }
        }

        public static void Generate(Action<List<Item>> onFinished)
        {
            var task = Server.GetItemsAsync();
            Scheduler.WaitUntil(() => task.IsCompleted, () =>
            {
                var itemsWrapper = JsonConvert.DeserializeObject<ServerResponseItemsContainerWrapper>(task.Result);
                var items = new List<Item>();
                foreach (var itemWrapper in itemsWrapper.Items)
                {
                    items.Add(new Item(itemWrapper));
                }

                onFinished?.Invoke(items);
            });
        }
    }
}
