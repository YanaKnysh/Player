using NAudio.Wave;
using System;
using System.Threading;
using System.IO;

namespace PlayerFinalVersion
{
    public class Player : IPlayable
    {
        private Playlist currentPlaylist;
        private readonly Order currentOrder;
        private AudioFileReader audioFileReader;
        private WaveOutEvent WaveOutEvent;
        private bool isPaused;
        private bool isStopped;
        
        public Player(Playlist currentPlaylist, Order currentOrder = Order.direct)
        {
            this.currentPlaylist = currentPlaylist;
            this.currentOrder = currentOrder;
        }

        public void Play()
        {
            if (WaveOutEvent?.PlaybackState == PlaybackState.Playing) // prevent second call
            {
                Console.WriteLine("Command is not active, player was already playing");
                return;
            }

            if (isStopped || isPaused)
            {
                isStopped = false;
                isPaused = false;
            }

            while (currentPlaylist.GetNext(currentOrder, out Song song))
            {
                if (WaveOutEvent?.PlaybackState != PlaybackState.Paused) //for Play to act like resume if called second time
                {
                    audioFileReader = new AudioFileReader(song.Path);
                    WaveOutEvent = new WaveOutEvent();
                    WaveOutEvent.Init(audioFileReader);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"{song.Artist.Name} - {song.Name}");
                    Console.WriteLine(song.Path);
                    Console.ForegroundColor = ConsoleColor.White;
                }

                WaveOutEvent.Play();

                do
                {
                    if (isStopped || isPaused)
                    {
                        return;
                    }
                }
                while (WaveOutEvent.PlaybackState == PlaybackState.Playing);
            }

            if (!currentPlaylist.GetNext(currentOrder, out Song nextSong) || currentOrder == Order.random) //to play again if finished
            {
                if (currentPlaylist.Songs.Count == 0)
                {
                    Console.WriteLine("There are no wav or mp3 files in directory");
                    return;
                }
                else
                {
                    currentPlaylist.IsStartSet = false;
                }
            }
        }

        public void Pause()
        {
            if (WaveOutEvent == null || WaveOutEvent?.PlaybackState != PlaybackState.Playing)
            {
                Console.WriteLine("Command is not active, player was not playing");
                return;
            }

            isPaused = true;
            WaveOutEvent.Pause();
        }

        public void Stop()
        {
            if (WaveOutEvent == null || WaveOutEvent?.PlaybackState == PlaybackState.Stopped)
            {
                Console.WriteLine("Command is not active, player was not playing");
                return;
            }

            isStopped = true;
            WaveOutEvent.Stop();
        }
        
        public void Exit()
        {
            WaveOutEvent?.Dispose();
            audioFileReader?.Dispose();
        }
    }
}
