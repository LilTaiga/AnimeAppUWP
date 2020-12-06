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

using System.Text.Json;

using AnimeAppUWP.Anilist;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AnimeAppUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void AnilistSubmitButton_Click(object sender, RoutedEventArgs e)
        {
            AnilistProgressRing.IsActive = true;
            AnilistNotFoundText.Visibility = Visibility.Collapsed;
            AnilistNotFoundText.Text = "User not found";

            string result = JsonSerializer.Serialize(new Query(AnilistTextBox.Text));
            QueryResult qResult = new QueryResult();

            try
            {
                Query userQuery = new Query(AnilistTextBox.Text);
                QueryResult userResult = await userQuery.SendRequest();

                Frame.Navigate(typeof(UserPage), userResult.data.User);
            }
            catch
            {
                AnilistNotFoundText.Visibility = Visibility.Visible;
                AnilistProgressRing.IsActive = false;
                return;
            }

            AnilistNotFoundText.Text = "Found!!!";
            AnilistNotFoundText.Visibility = Visibility.Visible;
            AnilistProgressRing.IsActive = false;
        }

        private void AnilistTextBox_GettingFocus(object sender, GettingFocusEventArgs args)
        {
            AnilistNotFoundText.Visibility = Visibility.Collapsed;
            AnilistProgressRing.IsActive = false;
        }
    }
}
