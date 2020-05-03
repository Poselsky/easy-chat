using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace easychat.Model
{
    public class Message
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("userID")]
        public string UserID { get; set; }
        public string MessageKey;

        public Message(string Text,  string UserID)
        {
            this.Text = Text;
            this.UserID = UserID;
        }

        public async Task<User> LinkedToUser()
        {
            return await User.GetUserByUid(this.UserID);
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
