using easychat.Model;
using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace easychat.Classes
{
    class ApplicationState
    {
        public static readonly FirebaseAuthProvider Provider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyCP-y3F1ULt-VOFX0op-skcL1a7m4HyTWI"));
        public static readonly FirebaseClient FirebaseClient = new FirebaseClient("https://easychat-3685d.firebaseio.com");

        private static ApplicationState _Instance;
        public static ApplicationState Instance
        {
            get
            {
                return _Instance == null ? _Instance = new ApplicationState() : _Instance;
            }
        }

        public FirebaseUser LoggedInUser { get; set; }
        public bool IsLoggedIn { get; set; } = false;

        public string CurrentlySelectedGroupId { get; set; }

        private ApplicationState() { }
    }
}
