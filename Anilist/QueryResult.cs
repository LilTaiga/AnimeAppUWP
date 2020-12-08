using System;
using System.Globalization;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

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

        public Media Media { get; set; }
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
        public int id { get; set; }
        public int? episodes { get; set; }
        public CoverImage coverImage { get; set; }
        public List<string> genres { get; set; }

        public List<string> synonyms { get; set; }
        public string description { get; set; }
        public StartDate startDate { get; set; }
        public EndDate endDate { get; set; }
        public string season { get; set; }
        public int? seasonYear { get; set; }
        public int? duration { get; set; }
        public int? averageScore { get; set; }
        public int? meanScore { get; set; }
        public List<Tag> tags { get; set; }
        public Studios studios { get; set; }
        public Relations relations { get; set; }

        public string GetAllTitles()
        {
            string result = "";

            if (!string.IsNullOrWhiteSpace(title.native))
                result += title.native + "\n";

            if (!string.IsNullOrWhiteSpace(title.romaji))
                result += title.romaji + "\n";

            if (!string.IsNullOrWhiteSpace(title.english))
                result += title.english;

            return result;
        }

        public string GetAllAlternativeTitles()
        {
            string result = "";

            foreach (string _title in synonyms)
                result += _title + "\n";

            return result;
        }

        public string GetDescription()
        {
            return Regex.Replace(description, "<[A-Za-z\\/\\ ]+>", "");
        }

        public string GetGenresListAsString()
        {
            string result = "";

            foreach (string _genre in genres)
                result += _genre + "\n";

            return result;
        }

        public int GetEpisodes()
        {
            if (episodes == null)
                return 0;

            return (int)episodes;
        }

        public double GetEpisodesAsDouble()
        {
            if (episodes == null)
                return 0;

            return (double)episodes;
        }

        public string GetEpisodesAsString()
        {
            if (episodes == null)
                return "0";

            return episodes.ToString();
        }

        public string GetEpisodesAndDuration()
        {
            string result = episodes + " episodes.\n";
            result += duration + " minutes each.";

            return result;
        }

        public string GetStudiosListAsString()
        {
            string result = "";

            foreach (Node _studio in studios.nodes)
                result += _studio.name + "\n";

            return result;
        }

        public string GetStartDateAsString()
        {
            if (startDate.day == null || startDate.month == null || startDate.year == null)
                return "Not aired yet.";

            return startDate.day + "/" + startDate.month + "/" + startDate.year;
        }

        public string GetEndDateAsString()
        {
            if (startDate.day == null || startDate.month == null || startDate.year == null)
                return "Not aired yet.";
            else if (endDate.day == null || endDate.month == null || endDate.year == null)
                return "Still airing.";

            return endDate.day + "/" + endDate.month + "/" + endDate.year;
        }

        public string GetSeasonAsString()
        {
            string result = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(season.ToLower());
            result += " ";
            result += seasonYear.ToString();

            return result;
        }

        public string GetAverageScoreAsString()
        {
            if (averageScore == null)
                return "Not aired yet.";

            return averageScore.ToString();
        }

        public string GetMeanScoreAsString()
        {
            if (meanScore == null)
                return "Not aired yet.";

            return meanScore.ToString();
        }
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

    public class StartDate
    {
        public int? year { get; set; }
        public int? month { get; set; }
        public int? day { get; set; }
    }

    public class EndDate
    {
        public int? year { get; set; }
        public int? month { get; set; }
        public int? day { get; set; }
    }

    public class Tag
    {
        public string name { get; set; }
        public int rank { get; set; }
        public bool isGeneralSpoiler { get; set; }
    }

    public class Node
    {
        public string name { get; set; }
    }

    public class Studios
    {
        public List<Node> nodes { get; set; }
    }

    public class Node2
    {
        public Title title { get; set; }
        public string type { get; set; }
        public string format { get; set; }
        public string status { get; set; }
        public CoverImage coverImage { get; set; }
    }

    public class Edge
    {
        public string relationType { get; set; } 
        public Node2 node { get; set; } 
    }

    public class Relations
    {
        public List<Edge> edges { get; set; }
    }
}
