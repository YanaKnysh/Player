using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayerFinalVersion
{
    public class Program
    {
        static void Main(string[] args)
        {
            FileReader fileReader = new FileReader();
            List<Song> songs = fileReader.GetSongsFromDirectory();
            Playlist playlist = new Playlist();
            playlist.AddSongs(songs);
            Player player = new Player(playlist, currentOrder: Order.direct);

            Console.WriteLine("Hot keys:");
            Console.WriteLine("P = Pause");
            Console.WriteLine("S = Play");
            Console.WriteLine("Q = Stop");
            Console.WriteLine("Esc = Exit");
           
            var quit = false;
            while (!quit)
            {
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.P:
                        Console.Write("\b \b"); // used to delete unnecessary symbol from console 
                        Console.WriteLine("Pause was pressed");
                        Task.Run(() => player.Pause());
                        break;
                    case ConsoleKey.S:
                        Console.Write("\b \b");
                        Console.WriteLine("Play was pressed");
                        Task.Run(() => player.Play());
                        break;
                    case ConsoleKey.Q:
                        Console.Write("\b \b");
                        Console.WriteLine("Stop was pressed");
                        Task.Run(() => player.Stop());
                        break;
                    case ConsoleKey.Escape:
                        Console.Write("\b \b");
                        Console.WriteLine("Exit was pressed");
                        player.Exit();
                        quit = true;
                        break;
                    default:
                        Console.Write("\b \b");
                        Console.WriteLine($"{key.Key} is not a command. Try again.");
                        break;
                }
            }
        }
    }
}
