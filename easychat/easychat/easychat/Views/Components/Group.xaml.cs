using easychat.Classes;
using easychat.Views.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace easychat.Views.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(true)]
    public partial class Group : ContentView, INotifyPropertyChanged
    {
        #region BINDABLE CommandProperty declaration
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(Group), null,
            propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
            {
                ((Group)bindable).Command = (ICommand)newValue;
            });
        #endregion
        public static readonly BindableProperty NameProperty = BindableProperty.Create(nameof(Name), typeof(string), typeof(Group), null);


        #region Command property declaration
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set
            {
                SetValue(CommandProperty, value);
                this.GestureRecognizers.Clear();
                var tap = new TapGestureRecognizer();
                tap.Tapped += (s, e) =>
                {
                    value.Execute(new GroupDetailPagePropagationInfo(Name,Key));
                };
                this.GestureRecognizers.Add(tap);
            }
        }
        #endregion
        public string Key;
        public string Name
        {
            get => (string)GetValue(NameProperty);
            set
            {
                SetValue(NameProperty, value);
            }
        } 

        public Group()
        {
            InitializeComponent();
        }
    }


}