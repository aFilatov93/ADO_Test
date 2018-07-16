using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using TagLib;
using System.Data.SqlClient;
using System.Data;

namespace ADO_testing_1
{
    class Program
    {
        // подключение к БД
        static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        static SqlConnection connection = new SqlConnection(connectionString);

        static void Main(string[] args)
        {
            // раскомментировать ниже, если нужно сначала вывести инфу о подключении к БД
            //DBManipulation.GetConnectionInfo();
            //Console.Read();

            // переменная для цикла
            bool switcher = true;

            // путь к папке с музыкой, для ввода пути вручную, раскомментировать строку в цикле
            string directory = @"E:\Blush Response";
            
            // цикл добавления информации в БД
            while (switcher)
            {
                //directory = Console.ReadLine();
                // если указанного пути не существует, отлавливает ошибку NullReferenceException, 
                // цикл перезапускается, нужно в случае если путь будет вводиться вручную
                try
                {
                    // если путь существует, создает список с AudioFile из указанных директорий
                    AudioFilesList testAu = new AudioFilesList(directory);

                    // если путь существует, но в нем нет мп3 файлов, перезапускает цикл
                    // опять таки, актуально если путь вводился вручную
                    if (testAu.tracks.Count > 0)
                    {
                        // просто проверка, 
                        // если программа сюда дошла, значит файлы есть и список заполнился
                        Console.WriteLine("Success1..");

                        // добавляет информацию из объектов списка в БД
                        foreach(var Au1 in testAu.tracks)
                        {
                            DBManipulation.InsertTrack(Au1);
                        }

                        // указанные ниже две процедуры, 
                        // нужны для того, чтобы в таблице Albums заполнились поля TracksCount
                        // а в Artists - AlbumCount. нужно заменить триггерами в БД
                        DBManipulation.AlbumsTrackCountSP();
                        DBManipulation.ArtistsAlbumsCountSP();

                        // все хорошо..
                        Console.WriteLine("Success2..");

                        // выход из цикла
                        switcher = false;
                    }
                    else Console.WriteLine("В папках нет .mp3 файлов");
                }
                catch (NullReferenceException ex)
                {
                    ex = new NullReferenceException();
                    Console.WriteLine("Null Reference");
                }
            }

            Console.Read();
        }
    }
}
