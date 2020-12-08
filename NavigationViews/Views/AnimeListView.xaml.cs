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
using AnimeAppUWP.Dialogs;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AnimeAppUWP.NavigationViews.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AnimeListView : Page
    {
        private Data profileData;

        private List<Entry> userEntries = new List<Entry>();
        public ObservableCollection<Entry> UserEntries { get; set; } = new ObservableCollection<Entry>();

        public AnimeListView()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            profileData = e.Parameter as Data;
            if (profileData == null)
                throw new Exception("Something went wrong :c");

            FillDisplayEntries(profileData.AnimePlanningList.lists.Find(_list => _list?.name == "Planning"));
            FillDisplayEntries(profileData.AnimeWatchingList.lists.Find(_list => _list?.name == "Watching"));
            FillDisplayEntries(profileData.AnimeRewatchingList.lists.Find(_list => _list?.name == "Rewatching"));
            DisplayEntries();
        }
        private void FillDisplayEntries(List _list)
        {
            if (_list == null)
                return;

            foreach (Entry _entry in _list.entries)
                userEntries.Add(_entry);
        }

        private void DisplayEntries()
        {
            userEntries.Sort();
            foreach (Entry _entry in userEntries)
                UserEntries.Add(_entry);
        }

        private async void EntriesList_ItemClick(object sender, ItemClickEventArgs e)
        {
            Entry _entry = e.ClickedItem as Entry;

            EntryInformation _selected = new EntryInformation(_entry.media);

            await _selected.ShowAsync();
        }
    }
}
