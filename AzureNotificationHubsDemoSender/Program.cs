using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.NotificationHubs;
using System.Configuration;

namespace AzureNotificationHubsDemoSender
{
    class Program
    {
        static string connectionstring = ConfigurationManager.AppSettings["connectionstring"];
        static string path = ConfigurationManager.AppSettings["path"];

        static void Main(string[] args)
        {            
            SendNotificationAsync();
            Console.ReadKey();           
        }

        private static async void SendNotificationAsync()
        {

            NotificationHubClient hub = NotificationHubClient.CreateClientFromConnectionString(connectionstring, path);
            var toast = @"<toast><visual><binding template=""ToastText01""><text id=""1"">Hello, World</text></binding></visual></toast>";

            await hub.SendWindowsNativeNotificationAsync(toast);
        }
    }
}
