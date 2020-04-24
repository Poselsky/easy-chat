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
        public static readonly BindableProperty GroupNameProperty = BindableProperty.Create(nameof(GroupName), typeof(string), typeof(GroupDetailPage), "Anonymous Group", BindingMode.TwoWay);

        public string GroupName
        {
            get => (string)GetValue(GroupNameProperty);
            set
            {
                SetValue(GroupNameProperty, value);
            }
        }

        public GroupDetailPage()
        {
            BindingContext = this;
            InitializeComponent();
        }
    }
}