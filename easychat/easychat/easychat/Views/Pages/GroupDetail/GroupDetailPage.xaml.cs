using easychat.Classes;
using easychat.Model;
using easychat.Views.Components;
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

        public GroupDetailPage()
        {
            BindingContext = this;
            InitializeComponent();
        }

        public void ListenToFirebaseGroupMessages()
        {
            App.FirebaseClient.Child("messages").Child(GroupInfo.KeyPage).AsObservable<Message>().Subscribe(
                (singleMessage) =>
                {
                    if(singleMessage.Object != null)
                    {
                        var allChildren = this.FetchedMessages.Children;
                        var singleGroupMessage = new GroupMessage();
                        singleGroupMessage.Message = singleMessage.Object;
                        singleGroupMessage.Message.MessageKey = singleMessage.Key;

                        GroupMessage viewInChild;
                        try
                        {
                            viewInChild = (GroupMessage)allChildren.First(view => ((GroupMessage)view).Message.MessageKey == singleMessage.Key);
                        }
                        catch (Exception e)
                        {
                            viewInChild = null;
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
    }
}