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
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(Group), null,
            propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
            {
                ((Group)bindable).Command = (ICommand)newValue;
            });

        public static readonly BindableProperty NameProperty = BindableProperty.Create(nameof(Name), typeof(string), typeof(Group), null);

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set
            {
                SetValue(CommandProperty, value);
                Main.GestureRecognizers.Clear();
                var tap = new TapGestureRecognizer();
                tap.Tapped += (s, e) =>
                {
                    value.Execute(new PagePropagationInfo(Name,Key));
                };
                Main.GestureRecognizers.Add(tap);
            }
        }

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

    public class PagePropagationInfo
    {
        public string PageName;
        public string KeyPage;
        public PagePropagationInfo(string PageName, string KeyPage)
        {
            this.KeyPage = KeyPage;
            this.PageName = PageName;
        }
    }
}