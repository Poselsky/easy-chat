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
using easychat.Classes;
using easychat.Model;

namespace easychat.Views.Pages
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(true)]
    public partial class GroupPage : ContentPage, INotifyPropertyChanged
    {
        private string _Input;
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

        public GroupPage()
        {
            InitializeComponent();
            BindingContext = this;
            Command = new Command(async (obj) => {
                var propagation = (GroupDetailPagePropagationInfo)obj;
                ApplicationState.Instance.CurrentlySelectedGroupId = propagation.KeyPage;
                await App.MasterDetailPage.ChangeGroupDetailPage(propagation);
            });
            ListenToFirebaseGroups();
        }

        public void ListenToFirebaseGroups()
        {
            ApplicationState.FirebaseClient.Child("groups").AsObservable<MessageGroup>().Subscribe(
                onNext: group =>
                {
                    var allChildren = this.Responses.Children;
                    var singleGroup = new Group();
                    singleGroup.Name = group.Object.GroupName;
                    singleGroup.Command = this.Command;
                    singleGroup.Key = group.Key;


                    Group viewInChild = null;
                    for (int i = 0; i < allChildren.Count; i++)
                    {
                        var temp = (Group)allChildren[i];
                        if (temp.Key == group.Key)
                        {
                            viewInChild = temp;
                        }
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
    }

}
