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
        private static readonly FirebaseAuthProvider provider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyCP-y3F1ULt-VOFX0op-skcL1a7m4HyTWI"));

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

        public MainMasterDetailPageDetail()
        {
            InitializeComponent();
        }

        private async void Register(object sender, EventArgs e)
        {
            var res = await provider.CreateUserWithEmailAndPasswordAsync(Email, Password, displayName: DisplayName);
            var photoUrl = res.User.PhotoUrl;
        }
        private async void LogIn(object sender, EventArgs e)
        {
            var res = await provider.SignInWithEmailAndPasswordAsync(Email, Password);
            var usrName = res.User.DisplayName;
            var photoUrl = res.User.PhotoUrl;
        }
    }
}