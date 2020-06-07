using easychat.Classes;
using easychat.Model;
using easychat.Views.Components;
using Firebase.Auth;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace easychat.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupDetailPage : ContentPage, INotifyPropertyChanged
    {
        public static readonly BindableProperty GroupInfoProperty = BindableProperty.Create(nameof(GroupInfo), typeof(GroupDetailPagePropagationInfo), typeof(GroupDetailPage), null, BindingMode.OneWay);

        public GroupDetailPagePropagationInfo GroupInfo
        {
            get => (GroupDetailPagePropagationInfo)GetValue(GroupInfoProperty);
            set
            {
                SetValue(GroupInfoProperty, value);
            }
        }

        private string _UserMessage;
        public string UserMessage
        {
            get => _UserMessage;
            set
            {
                _UserMessage = value;
                OnPropertyChanged(nameof(UserMessage));
            }
        }

        public GroupDetailPage()
        {
            BindingContext = this;
            InitializeComponent();
        }

        public void ListenToFirebaseGroupMessages()
        {
            ApplicationState.FirebaseClient.Child("messages").Child(GroupInfo.KeyPage).AsObservable<Message>().Subscribe(
                (singleMessage) =>
                {
                    if(singleMessage.Object != null)
                    {
                        var allChildren = this.FetchedMessages.Children;
                        var singleGroupMessage = new GroupMessage();
                        singleGroupMessage.Message = singleMessage.Object;
                        singleGroupMessage.Message.MessageKey = singleMessage.Key;

                        GroupMessage viewInChild = null;
                        for(int i = 0; i < allChildren.Count; i++)
                        {
                            var temp = (GroupMessage)allChildren[i];
                            if (temp.Message.MessageKey == singleMessage.Key)
                            {
                                viewInChild = temp;
                            }
                        }

                        Dispatcher.BeginInvokeOnMainThread(() =>
                        {
                            switch (singleMessage.EventType)
                            {
                                case Firebase.Database.Streaming.FirebaseEventType.Delete:
                                    allChildren.Remove(viewInChild);
                                    break;
                                default:
                                    if (viewInChild == null)
                                    {
                                        allChildren.Add(singleGroupMessage);
                                    }
                                    break;
                            }
                        });
                    }
                }
           );
        }

        private async void Send_Event(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(UserMessage))
            {
                await ApplicationState.FirebaseClient.Child("messages").Child(GroupInfo.KeyPage).PostAsync(
                    new Message(UserMessage, ApplicationState.Instance.LoggedInUser.UserName)
                );
            }
        }
    }
}