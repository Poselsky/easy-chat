using easychat.Classes;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace easychat.Model
{
    public class FirebaseUser
    {
        public string UserName { get; set; }
        public string Registered { get; set; }
        public string ProfilePictureUrl { get; set; }

        [JsonIgnore]
        public DateTime RegisteredDateTime { 
            get
            {
                return DateTime.Parse(Registered);
            } 
        }

        [JsonIgnore]
        public string UID { get; set; }

        [JsonConstructor]
        public FirebaseUser (string UserName, string Registered, string ProfilePictureUrl)
        {
            this.UserName = UserName;
            this.Registered = Registered;
            this.ProfilePictureUrl = ProfilePictureUrl;
        }

        public static async Task<FirebaseUser> GetUserByUid(string UID)
        {
            var user = await ApplicationState.FirebaseClient.Child("users").Child(UID).OnceSingleAsync<FirebaseUser>();
            user.UID = UID;
            return user;
        }
    }
}
