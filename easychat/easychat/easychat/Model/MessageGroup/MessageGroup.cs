using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace easychat.Model
{
    public class MessageGroup
    {
        [JsonProperty("name")]
        public string GroupName { get; set; }
        public MessageGroup(string GroupName)
        {
            this.GroupName = GroupName;
        }
    }
}
