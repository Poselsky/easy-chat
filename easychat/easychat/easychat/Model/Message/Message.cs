using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

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
    }
}
