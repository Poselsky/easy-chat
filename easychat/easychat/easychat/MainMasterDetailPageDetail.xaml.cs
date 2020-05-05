using easychat.Classes;
using easychat.Model;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace easychat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMasterDetailPageDetail : ContentPage, INotifyPropertyChanged
    {
        private string _DisplayName;
        public string DisplayName
        {
            get => _DisplayName;
            set
            {
                _DisplayName = value;
                OnPropertyChanged(nameof(DisplayName));
            }
        }

        private string _Email;
        public string Email
        {
            get => _Email;
            set
            {
                _Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _Password;
        public string Password
        {
            get => _Password;
            set
            {
                _Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _AppLogInException;
        public string AppLogInException
        {
            get => _AppLogInException;
            set
            {
                _AppLogInException = value;
                OnPropertyChanged(nameof(AppLogInException));
            }
        }

        public bool IsLoggedIn
        {
            get => ApplicationState.Instance.IsLoggedIn;
            set
            {
                ApplicationState.Instance.IsLoggedIn = value;
                OnPropertyChanged(nameof(IsLoggedIn));
            }
        }

        public MainMasterDetailPageDetail()
        {
            InitializeComponent();
        }

        private async void Register(object sender, EventArgs e)
        {
            try
            {
                CheckForm(true);
                var res = await ApplicationState.Provider.CreateUserWithEmailAndPasswordAsync(Email, Password, displayName: DisplayName);

                User user = res.User;


                ApplicationState applicationState = ApplicationState.Instance;
                applicationState.LoggedInUser = new FirebaseUser(user.DisplayName, null, user.PhotoUrl);
                IsLoggedIn = true;
            } catch (Exception exc)
            {
                AppLogInException = exc.Message;
            }
        }
        private async void LogIn(object sender, EventArgs e)
        {
            try
            {
                CheckForm();
                var res = await ApplicationState.Provider.SignInWithEmailAndPasswordAsync(Email, Password);
                User user = res.User;


                ApplicationState applicationState = ApplicationState.Instance;
                applicationState.LoggedInUser = new FirebaseUser(user.DisplayName, null, user.PhotoUrl);
                applicationState.LoggedInUser.UID = user.LocalId;
                IsLoggedIn = true;
            } catch (Exception exc)
            {
                AppLogInException = exc.Message;
            }

        }

        private void CheckForm (bool checkDisplayName = false)
        {
            if (checkDisplayName)
                if (string.IsNullOrWhiteSpace(DisplayName))
                    throw new Exception("Please fill in user name");

            if (string.IsNullOrWhiteSpace(Email))
                throw new Exception("Please fill in email");

            if (string.IsNullOrWhiteSpace(Password) && Password.Length < 6)
                throw new Exception("Please fill in password. It's length must be bigger than 6");
        }
    }
}