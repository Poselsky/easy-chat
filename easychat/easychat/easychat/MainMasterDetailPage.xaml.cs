using easychat.Classes;
using easychat.Views.Components;
using easychat.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace easychat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMasterDetailPage : MasterDetailPage
    {
        public MainMasterDetailPage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainMasterDetailPageMasterMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }

        public async Task ChangeGroupDetailPage(GroupDetailPagePropagationInfo pagePropagationInfo)
        {
            var page = (GroupDetailPage)Activator.CreateInstance(typeof(GroupDetailPage));
            page.Title = pagePropagationInfo.PageName;
            page.GroupInfo = pagePropagationInfo;
            page.ListenToFirebaseGroupMessages();

            await ((NavigationPage)Detail).Navigation.PushAsync(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}