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
    public partial class Group : ContentView
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(Group), null,
            propertyChanged: (BindableObject bindable, object oldValue, object newValue) =>
            {
                ((Group)bindable).Command = (ICommand)newValue;
            });

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set
            {
                SetValue(CommandProperty, value);
                var tap = new TapGestureRecognizer();
                tap.Tapped += (s, e) =>
                {
                    value.Execute(s);
                };
                this.GestureRecognizers.Add(tap);
            }
        }

        public Group()
        {
            InitializeComponent();
        }

    }
}