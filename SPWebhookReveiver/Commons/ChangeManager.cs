using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPWebhookReveiver.Models;
using Newtonsoft.Json;
using Microsoft.SharePoint.Client;
using Microsoft.WindowsAzure;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Queue; // Namespace for Queue storage types


namespace SPWebhookReveiver.Commons
{
    public class ChangeManager
    {
        public const string StorageQueueName = "sharepointwebhookevent";
        public void AddNotificationToQueue(string storageConnectionString, SPWebhookNotification notification)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference(ChangeManager.StorageQueueName);

            queue.CreateIfNotExists();

            queue.AddMessage(new CloudQueueMessage(JsonConvert.SerializeObject(notification)));

        }
    }
}