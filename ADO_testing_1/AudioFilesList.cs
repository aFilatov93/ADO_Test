using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagLib;

namespace ADO_testing_1
{
    /// <summary>
    /// Класс, описывающий список объектов AudioFile,
    /// может выводит поля AudioFile в консоль, записывать и показывать пути к файлам
    /// </summary>
    class AudioFilesList
    {
        // путь к директории где хранятся мп3 файлы
        private string dir;
        // строковый массив со списком папок, включая подпапки
        private string[] subDirs;
        // строковый массив со списком путей к мп3 файлам
        private string[] tracksPaths;
        // основной список объектов AudioFile (см. класс AudioFile)
        public List<AudioFile> tracks { get; private set; }
        

        /// <summary>
        /// Конструктор, принимает путь к папке в которой должны быть мп3 файлы
        /// </summary>
        /// <param name="directory"></param>
        public AudioFilesList(string directory)
        {
            // присвоение введенного пути полю dir
            dir = directory;
            // если дерево папок не содержит мп3 файлов, выводит ошибку в консоль
            try
            {
                subDirs = Directory.GetFiles(@"" + dir, "*.mp3", SearchOption.AllDirectories);
            }
            catch (ArgumentException ex)
            {
                ex = new ArgumentException("Неправильный путь","");
                Console.WriteLine(ex.Message);
                return;
            }
            // добавление объектов AudioFile в список
            tracks  = subDirs.Select(file => new AudioFile(file)).ToList();
            // добавление путей к файлам в массив
            tracksPaths = new string[tracks.Count];
        }

        /// <summary>
        /// Выводит в консоль поля всех AudioFile из списка tracks
        /// </summary>
        public void ShowTags()
        {
            if (tracks.Count > 0)
            {
                foreach (var a in tracks)
                {
                    a.ShowTags();
                    Console.WriteLine();
                }
            }
            else Console.WriteLine("Список пуст");
        }

        /// <summary>
        /// Выводит в консоль пути к файлам
        /// </summary>
        public void ShowFilesDirectories()
        {
            if (tracks.Count > 0)
            {
                for (var i = 0; i < tracks.Count; i++)
                {
                    tracksPaths[i] = tracks[i].path;
                }

                foreach (var a in tracksPaths)
                {
                    Console.WriteLine(a);
                }
            }
            else Console.WriteLine("Список пуст");
        }

        /// <summary>
        /// Записывает в текстовый файл пути к файлам в корневой директории
        /// </summary>
        public void CreateFilesDirectoriesTxt()
        {
            if (tracks.Count > 0)
            {
                for (var i = 0; i < tracks.Count; i++)
                {
                    tracksPaths[i] = tracks[i].path;
                }

                System.IO.File.WriteAllLines(@"" + dir + "/Total.txt", tracksPaths);

                Console.WriteLine("Total.txt создан в: {0}.", dir);
            }
            else Console.WriteLine("Список пуст");
        }
    }
}
