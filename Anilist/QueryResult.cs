using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AnimeAppUWP.Anilist
{
    public class QueryResult
    {
        public Data data { get; set; }
    }
    public class Data
    {
        public User User { get; set; }
        public MediaListCollection AnimeWatchingList { get; set; }
        public MediaListCollection AnimePlanningList { get; set; }
        public MediaListCollection AnimeCompletedList { get; set; }
        public MediaListCollection AnimeDroppedList { get; set; }
        public MediaListCollection AnimePausedList { get; set; }
        public MediaListCollection AnimeRewatchingList { get; set; }
        public MediaListCollection MangaReadingList { get; set; }
        public MediaListCollection MangaPlanningList { get; set; }
        public MediaListCollection MangaCompletedList { get; set; }
        public MediaListCollection MangaDroppedList { get; set; }
        public MediaListCollection MangaPausedList { get; set; }
        public MediaListCollection MangaRereadingList { get; set; }
    }

    public class User
    {
        public string name { get; set; }
        public int id { get; set; }
        public Avatar avatar { get; set; }
    }

    public class Avatar
    {
        public string medium { get; set; }
        public string large { get; set; }
    }

    public class MediaListCollection
    {
        public List<List> lists { get; set; }
    }

    public class List
    {
        public string name { get; set; }
        public string status { get; set; }
        public List<Entry> entries { get; set; }
    }

    public class Entry : IComparer<Entry>, IComparable<Entry>
    {
        public double score { get; set; }
        public int progress { get; set; }
        public Media media { get; set; }

        [JsonIgnore]
        public string EntryTitle { get { return media.title.romaji; }}

        [JsonIgnore]
        public List<string> EntryGenres { get { return media.genres; }}

        [JsonIgnore]
        public string EntryGenresSingleString
        {
            get
            {
                string result = "";
                foreach (string _genre in media.genres)
                    result += _genre + ", ";

                result = result.Remove(result.Length - 2);
                return result;
            }
        }

        [JsonIgnore]
        public string EntryCoverMedium{ get { return media.coverImage.medium; }}

        public int Compare(Entry obj1, Entry obj2)
        {
            int stringComparison = string.Compare(obj1.EntryTitle, obj2.EntryTitle, System.StringComparison.OrdinalIgnoreCase);

            if (stringComparison > 0)
                return 1;
            if (stringComparison < 0)
                return -1;

            return 0;
        }

        public int CompareTo(Entry entry)
        {
            if (entry == null)
                return 1;

            return string.Compare(this.EntryTitle, entry.EntryTitle, StringComparison.OrdinalIgnoreCase);
        }
    }

    public class Media
    {
        public Title title { get; set; }
        public int? episodes { get; set; }
        public CoverImage coverImage { get; set; }
        public List<string> genres { get; set; }
    }

    public class CoverImage
    {
        public string medium { get; set; }
        public string large { get; set; }
        public string extraLarge { get; set; }
    }

    public class Title
    {
        public string english { get; set; }
        public string romaji { get; set; }
        public string native { get; set; }
    }
}
