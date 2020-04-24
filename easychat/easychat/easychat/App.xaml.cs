﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace easychat
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainMasterDetailPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
