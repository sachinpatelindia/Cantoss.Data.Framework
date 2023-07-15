using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cantoss.Data.Framework
{
    public abstract class CommonEntity
    {
        [JsonProperty(PropertyName = "id")]
        public  string Id { get; set; }
        [JsonProperty(PropertyName = "partitionKey")]
        public string PartitionKey { get; set; } 
    }
}