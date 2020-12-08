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

using AnimeAppUWP.Anilist;
using System.Reflection;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AnimeAppUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserPage : Page
    {
        Data profileData;
        private NavigationViewItem lastItem;

        public UserPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            profileData = e.Parameter as Data;
            if (profileData == null)
            {
                Frame.Navigate(typeof(MainPage));
                return;
            }

            NavigationView.PaneTitle = " ";
            NavigationViewUsername.Text = profileData.User.name;
            NavigationViewAvatar.ImageSource = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(profileData.User.avatar.medium));

            /*profileData.AnimeWatchingList = e.Parameter..AnimeWatchingList;
            profileData.AnimeRewatchingList = result.data.AnimeRewatchingList;
            profileData.AnimePlanningList = result.data.AnimePlanningList;
            profileData.AnimeCompletedList = result.data.AnimeCompletedList;
            profileData.AnimePausedList = result.data.AnimePausedList;
            profileData.AnimeDroppedList = result.data.AnimeDroppedList;*/

            /*profileData.MangaReadingList = result.data.MangaReadingList;
            profileData.MangaRereadingList = result.data.MangaRereadingList;
            profileData.MangaPlanningList = result.data.MangaPlanningList;
            profileData.MangaCompletedList = result.data.AnimeCompletedList;
            profileData.MangaPausedList = result.data.MangaPausedList;
            profileData.MangaDroppedList = result.data.MangaDroppedList;*/

            NavigateToView("WatchingView");
            NavigationView.SelectedItem = NavigationView.MenuItems[1];
        }

        private void NavigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var selectedItem = args.InvokedItemContainer as NavigationViewItem;

            if (selectedItem == null || lastItem == selectedItem)
                return;

            var pageName = selectedItem.Tag?.ToString();

            if (!NavigateToView(pageName))
                return;

            lastItem = selectedItem;
        }

        private bool NavigateToView(string _name)
        {
            var view = Assembly.GetExecutingAssembly().GetType($"AnimeAppUWP.NavigationViews.Views.{_name}");

            if (string.IsNullOrWhiteSpace(_name) || view == null)
                return false;

            NavigationViewFrame.Navigate(view, profileData);
            return true;
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {

        }

        private void NavigationViewItem_Leave(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
