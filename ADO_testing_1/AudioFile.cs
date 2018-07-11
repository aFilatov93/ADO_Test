using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagLib;

namespace ADO_testing_1
{
    internal class AudioFile
    {
        private string album;
        private string artist;
        private string title;
        private string genre;
        private object year;
        private object duration;

        public string Path { get; set; }

        public AudioFile(string path)
        {
            Path = path;
            var audioFile = TagLib.File.Create(path);

            album = audioFile.Tag.Album;
            artist = string.Join(", ", audioFile.Tag.Performers);
            title = audioFile.Tag.Title;
            genre = audioFile.Tag.Genres[0];
            year = audioFile.Tag.Year;
            duration = audioFile.Properties.Duration.ToString("hh\\:mm\\:ss");
        }

        public void ShowTags()
        {
            Console.WriteLine("Альбом: {0}\nИсполнитель: {1}\nНазвание: {2}\nГод: {3}\nДлительность: {4}\nЖанр: {5}",
                                album, artist, title, year, duration, genre);
        }

        public void ShowPath()
        {
            Console.WriteLine("Путь к файлу: {0}", Path);
        }

        public void TracksToList()
        {
            const string dir = @"C:\Mus";

            // поиск всех .mp3 файлов, включая подпапки по пути в dir
            var subDirs = Directory.GetFiles(@"" + dir, "*.mp3", SearchOption.AllDirectories);

            // выбор всех .mp3 файлов
            var tracks = subDirs.Select(file => new AudioFile(file)).ToList();

            int count = 0;

            foreach (var a in tracks)
            {
                count++;
                a.ShowTags();
                Console.WriteLine();
            }
            Console.WriteLine("Total: {0}", count);
            Console.Read();
        }

    }
}
