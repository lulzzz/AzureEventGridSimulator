﻿using Newtonsoft.Json;

namespace AzureEventGridSimulator
{
    public class SubscriptionSettings
    {
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Endpoint { get; set; }
    }
}