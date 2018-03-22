﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Praca_Inzynierska.Droid
{
    [Activity(
        Label = "MedControl",
        Icon = "@drawable/appicontry",
        Theme = "@style/MainTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)
        ]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            this.Window.AddFlags(WindowManagerFlags.Fullscreen);
            this.Window.AddFlags(WindowManagerFlags.KeepScreenOn);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            var x = typeof(Xamarin.Forms.Themes.DarkThemeResources);
            x = typeof(Xamarin.Forms.Themes.LightThemeResources);
            x = typeof(Xamarin.Forms.Themes.Android.UnderlineEffect);
        }
    }
}

