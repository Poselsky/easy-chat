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
using Xamarin.Forms.Internals;
using Newtonsoft.Json;
using easychat.Views.Components;
using System.Reactive.Linq;
using System.Threading;
using Newtonsoft.Json.Linq;

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

        private FirebaseClient FirebaseCli;

        public GroupPage()
        {
            InitializeComponent();
            BindingContext = this;
            Input = "FUU";
            FirebaseCli = new FirebaseClient("https://easychat-3685d.firebaseio.com");
            Command = new Command(async (obj) => {
                Debug.WriteLine(obj);
                await App.MasterDetailPage.ChangeDetailPage(typeof(GroupDetailPage), (PagePropagationInfo)obj);
            });
            GetAllGroups();
        }

        public async void GetAllGroups()
        {
            FirebaseCli.Child("groups").AsObservable<MessageGroup>().Subscribe(
                onNext: group =>
                {
                    var allChildren = this.Responses.Children;
                    var singleGroup = new Group();
                    singleGroup.Name = group.Object.GroupName;
                    singleGroup.Command = this.Command;
                    singleGroup.Key = group.Key;

                    Group viewInChild;
                    try
                    {
                        viewInChild = (Group)allChildren.First(view => ((Group)view).Key == group.Key);
                    } catch(Exception e)
                    {
                        viewInChild = null;
                    }

                    Dispatcher.BeginInvokeOnMainThread(() =>
                    {
                        switch (group.EventType)
                        {
                            case Firebase.Database.Streaming.FirebaseEventType.Delete:
                                allChildren.Remove(viewInChild);
                                break;
                            default:
                                if (viewInChild == null)
                                {
                                    allChildren.Add(singleGroup);
                                }
                                break;
                        }
                    });
                }
            );
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await FirebaseCli.Child("messages").PostAsync<Message>(new Message(this.Input));
        }

        #region propertyChanged section
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    public class Message
    {
        public string Input;
        public Message(string input)
        {
            this.Input = input;
        }
    }

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
