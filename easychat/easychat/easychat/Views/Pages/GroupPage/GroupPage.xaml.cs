using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Firebase.Database;
using Firebase.Database.Query;
using System.Diagnostics;
using Xamarin.Forms.Xaml;

namespace easychat.Views.Pages
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(true)]
    public partial class GroupPage : ContentPage, INotifyPropertyChanged
    {
        private string _Input = "haha";
        public string Input
        {
            get => _Input;
            set
            {
                this._Input = value;
                OnPropertyChanged(nameof(Input));
            }
        }

        public Command _Command;
        public Command Command 
        {
            get => _Command;
            set
            {
                _Command = value;
                OnPropertyChanged(nameof(Command));
            }
        }  

        private FirebaseClient Firebase;

        public GroupPage()
        {
            InitializeComponent();
            BindingContext = this;
            Input = "FUU";
            Firebase = new FirebaseClient("https://easychat-3685d.firebaseio.com");
            Command = new Command((obj) => {
                Debug.WriteLine(obj); 
            });
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Firebase.Child("messages").PostAsync<Message>(new Message(this.Input));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Message
    {
        public string Input;
        public Message(string input)
        {
            this.Input = input;
        }
    }
}
