using Microsoft.Azure.EventHubs;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMachineSimulator.Receiver
{
    public class Program
    {
        const string evConnString = "Endpoint=sb://coffemachineeventhubns.servicebus.windows.net/;SharedAccessKeyName=coffemachinepolicy;SharedAccessKey=z/YG/0fEEgVte1nj/2dzwhKw/weEtXRcBZp3RBBlNvg=;EntityPath=coffeemachineeventhub";

        static void Main(string[] args)
        {
            SubscribeToEventHubAsync().Wait();
        }

        private static async Task SubscribeToEventHubAsync()
        {
            Console.WriteLine("Connect to event hub");

            var eventHubClient = EventHubClient.CreateFromConnectionString(evConnString);

            var runtimeInformation = await eventHubClient.GetRuntimeInformationAsync();
            var partitionReceivers = runtimeInformation.PartitionIds
                .Select(x =>
                eventHubClient.CreateReceiver("$Default", x, EventPosition.FromEnqueuedTime(DateTime.Now))).ToList();

            Console.WriteLine("Wait for incoming events");

            foreach (var partitionReceiver in partitionReceivers)
            {
                partitionReceiver.SetReceiveHandler(new CoffeeEventReceiver(partitionReceiver.PartitionId));
            }

            Console.WriteLine("Please press any key to close the program");
            Console.ReadLine();

            await eventHubClient.CloseAsync();
        }
    }
}
