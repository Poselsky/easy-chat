using easychat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace easychat.Views.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupMessage : Grid
    {
        public static readonly BindableProperty MessageProperty = BindableProperty.Create(nameof(Message), typeof(Message), typeof(GroupMessage));

        public Message Message
        {
            get => (Message)GetValue(MessageProperty);
            set
            {
                SetValue(MessageProperty, value);
            }
        }

        public GroupMessage()
        {
            BindingContext = this;
            InitializeComponent();
        }

    }
}