using System;
using System.Collections.Generic;

namespace PlayerFinalVersion
{
    public class Playlist
    {
        public List<Song> Songs { get; set; }
        public int CurrentSong { get; private set; }
        public bool IsStartSet { get; set; }
        private int count;
        private static Random random;

        static Playlist()
        {
            random = new Random();
        }

        public Playlist()
        {
            Songs = new List<Song>();
        }

        public void AddSong(Song song)
        {
            Songs.Add(song);
        }

        public void AddSongs(List<Song> songs)
        {
            Songs.AddRange(songs);
        }

        public bool GetNext(Order order, out Song nextSong)
        {
            if(!IsStartSet)
            {
                StartSet(order);
            }

            nextSong = null;

            switch (order)
            {
                case Order.direct:
                    if (CurrentSong >= Songs.Count)
                    {
                        return false;
                    }
                    nextSong = Songs[CurrentSong];
                    CurrentSong++;
                    return true;
                case Order.reverse:
                    if (CurrentSong < 0)
                    {
                        return false;
                    }
                    nextSong = Songs[CurrentSong];
                    CurrentSong--;
                    return true;
                case Order.random:
                    if(count >= Songs.Count)
                    {
                        return false;
                    }
                    CurrentSong = random.Next(Songs.Count);
                    nextSong = Songs[CurrentSong];
                    count++;
                    return true;
                default:
                    return false;
            }
        }

        public void SetCurrentSong(int currentSong)
        {
            CurrentSong = currentSong;
        }

        public int GetCurrentSongIndex()
        {
            return CurrentSong;
        }

        private void StartSet(Order order)
        {
            if (!IsStartSet)
            {
                if(order == Order.reverse)
                {
                    CurrentSong = Songs.Count - 1;
                }
                else
                {
                    CurrentSong = 0;
                }

                IsStartSet = true;
                count = 0;
            }
        }
    }
}