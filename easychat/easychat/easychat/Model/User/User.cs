using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace easychat.Model
{
    public class User
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

        [JsonConstructor]
        public User (string UserName, string Registered, string ProfilePictureUrl)
        {
            this.UserName = UserName;
            this.Registered = Registered;
            this.ProfilePictureUrl = ProfilePictureUrl;
        }

        public static Task<User> GetUserByUid(string UID)
        {
            return App.FirebaseClient.Child("users").Child(UID).OnceSingleAsync<User>();
        }
    }
}
