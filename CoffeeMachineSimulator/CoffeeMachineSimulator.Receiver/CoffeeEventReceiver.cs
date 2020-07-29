using Microsoft.Azure.EventHubs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachineSimulator.Receiver
{
    public class CoffeeEventReceiver : IPartitionReceiveHandler
    {
        private string partitionId;

        public CoffeeEventReceiver(string partitionId)
        {
            this.partitionId = partitionId;
        }

        public int MaxBatchSize { get => 10; set => throw new NotImplementedException(); }

        public Task ProcessErrorAsync(Exception error)
        {
            throw new NotImplementedException();
        }

        public Task ProcessEventsAsync(IEnumerable<EventData> events)
        {
            if(events != null)
            {
                foreach(var eventData in events){
                    var dataAsJson = Encoding.UTF8.GetString(eventData.Body.Array);
                    Console.WriteLine(dataAsJson);
                }
            }

            return Task.CompletedTask;
        }
    }
}
