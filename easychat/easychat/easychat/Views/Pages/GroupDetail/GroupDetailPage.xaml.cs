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
        public GroupDetailPage()
        {
            InitializeComponent();
        }

        #region propertyChanged section
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}