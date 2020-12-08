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
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AnimeAppUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserPageLoading : Page
    {
        public UserPageLoading()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Data profileData = new Data();

            profileData.User = e.Parameter as User;
            if (profileData.User == null)
            {
                Frame.Navigate(typeof(MainPage));
                return;
            }

            var result = await DownloadAnimeList(profileData.User.id);

            result.data.User = profileData.User;

            Frame.Navigate(typeof(UserPage), result.data);
        }

        private async Task<QueryResult> DownloadAnimeList(int _id)
        {
            var watchingQuery = new Query(_id);
            var result = await watchingQuery.SendRequest();

            return result;
        }
    }
}
