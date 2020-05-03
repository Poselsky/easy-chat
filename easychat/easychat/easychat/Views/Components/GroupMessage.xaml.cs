using easychat.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace easychat.Views.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupMessage : Grid, INotifyPropertyChanged
    {
        public static readonly BindableProperty MessageProperty = BindableProperty.Create(nameof(Message), typeof(Message), typeof(GroupMessage));

        public Message Message
        {
            get => (Message)GetValue(MessageProperty);
            set
            {
                SetValue(MessageProperty, value);
                if (value != null)
                    FetchUser();
            }
        }

        private User _LinkedToUser;
        public User LinkedToUser
        {
            get => _LinkedToUser;
            set
            {
                this._LinkedToUser = value;
                OnPropertyChanged(nameof(LinkedToUser));
            }
        }

        public GroupMessage()
        {
            BindingContext = this;
            InitializeComponent();
        }

        private async void FetchUser()
        {
            LinkedToUser = await Message.LinkedToUser();
        }

    }
}