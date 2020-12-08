using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using AnimeAppUWP.Dialogs.Views;
using AnimeAppUWP.Anilist;
using System.Reflection;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AnimeAppUWP.Dialogs
{
    public sealed partial class EntryInformation : ContentDialog
    {
        private Media data;
        public EntryInformation(Media _media)
        {
            this.InitializeComponent();
            data = _media;

            NavView.SelectedItem = NavView.MenuItems[0];
        }

        private void ContentDialog_CloseButtonClick(Windows.UI.Xaml.Controls.ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var _name = args.SelectedItemContainer.Tag;
            var view = Assembly.GetExecutingAssembly().GetType($"AnimeAppUWP.Dialogs.Views.{_name}");

            EntryInformationFrame.Navigate(view != null ? view : typeof(MainPage), data);
        }
    }
}
