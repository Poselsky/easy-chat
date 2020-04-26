using Newtonsoft.Json;
using System;

namespace easychat.Model
{
    public class Message
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }
        public string MessageKey;
        public Message(string Text,  string UserName)
        {
            this.Text = Text;
            this.UserName = UserName;
        }
        /*
        public Message(string Text, string UserName, string MessageKey)
        {
            this.Text = Text;
            this.UserName = UserName;
            this.MessageKey = MessageKey;
        }
        */
    }
}
