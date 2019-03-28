using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerFinalVersion
{
    public class FileReader
    {
        public List<Song> GetSongsFromDirectory()
        {
            List<FileInfo> files = GetFilesFromDirectory();
            List<Song> songs = new List<Song>();

            if (files == null)
            {
                Console.WriteLine("Folder doesn't contain mp3 or wav files");
            }

            foreach (var file in files)
            {
                TagLib.File tagFile = TagLib.File.Create(file.FullName);
                string name = tagFile.Tag.Title;
                string artistName = null;

                for (int i = 0; i < tagFile.Tag.Performers.Length; i++)
                {
                    artistName += tagFile.Tag.Performers[i];
                }

                Artist artist = new Artist(artistName);
                Album album = new Album(tagFile.Tag.Album, (int)tagFile.Tag.Year);
                int year = (int)tagFile.Tag.Year;
                TimeSpan length = tagFile.Properties.Duration;
                string path = file.FullName;
                Song song = new Song(name, artist, album, year, length, path);
                songs.Add(song);
            }

            return songs;
        }

        private List<FileInfo> GetFilesFromDirectory()
        {
            string path = ConfigurationManager.AppSettings["Path"];
            string[] extensions = ConfigurationManager.AppSettings["Extension"].Split(',');

            DirectoryInfo directory = new DirectoryInfo(path);

            if (!directory.Exists)
            {
                Console.WriteLine("Directory doesn't exist");
                return null;
            }

            List<FileInfo> files = new List<FileInfo>();

            foreach (var file in directory.GetFiles())
            {
                for (int i = 0; i < extensions.Length; i++)
                {
                    if (file.FullName.EndsWith(extensions[i]))
                    {
                        files.Add(file);
                    }
                }
            }

            return files;
        }
    }
}
