using System;

namespace PlayerFinalVersion
{
    public class Song
    {
        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                if (value == null)
                {
                    name = "unknown title";
                }
                else
                {
                    name = value;
                }
            }
        }

        public Artist Artist { get; set; }
        public Album Album { get; set; }
        public int Year { get; private set; }
        public TimeSpan Length { get; private set; }
        public string Path { get; private set; }
        private string name;

        public Song(string name, Artist artist, Album album, int year, TimeSpan length, string path)
        {
            Name = name;
            Artist = artist;
            Album = album;
            Year = year;
            Length = length;
            Path = path;
        }
    }
}