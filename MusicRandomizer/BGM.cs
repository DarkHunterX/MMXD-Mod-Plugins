using System;
using System.Collections.Generic;

namespace MusicRandomizer
{
    public class BGM
    {
        static List<string> TrackList = new();

        public class Tracks
        {
            public List<string> BGM00 { get; set; }
            public List<string> BGM01 { get; set; }
            public List<string> BGM02 { get; set; }
            public List<string> BGM03 { get; set; }
        }

        public static (string acb, string song) GetRandomSong(Tracks list)
        {
            var random = new Random();
            var s_acb = $"BGM0{random.Next(4)}";
            TrackList = list.BGM00;
            switch (s_acb)
            {
                case "BGM01":
                    TrackList = list.BGM01;
                    break;
                case "BGM02":
                    TrackList = list.BGM02;
                    break;
                case "BGM03":
                    TrackList = list.BGM03;
                    break;
            }
            // pick random song from list
            return (acb: s_acb, song: TrackList[random.Next(TrackList.Count)]);
        }


    }
}
