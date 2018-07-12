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
        static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        static SqlConnection connection = new SqlConnection(connectionString);

        static void Main(string[] args)
        {
            //InsertArtist();
            //GetArtistsWithCountries();
            //GetCountries();
            //GetConnectionInfo();
            //DBManipulation.GetConnectionInfo();

            //TEST();

            bool switcher = true;
            
            while (switcher)
            {
                //string directory = Console.ReadLine();
                string directory = @"E:\Mus";

                try
                {
                    AudioFilesList testAu = new AudioFilesList(directory);
                    if (testAu.Tracks.Count > 0)
                    {
                        //testAu.ShowTags();
                        Console.WriteLine("Success1..");

                        var Au1 = testAu.Tracks[0];

                        InsertTrack(Au1.Album, Au1.Artist, Au1.Title, Au1.Genre, Au1.Year, Au1.Duration, Au1.TrackNumber);

                        Console.WriteLine("Success2..");

                        switcher = false;
                    }
                    else Console.WriteLine("Folders doesn't contain .mp3 files");
                }
                catch (NullReferenceException ex)
                {
                    ex = new NullReferenceException();
                    Console.WriteLine("Null Reference");
                }
            }

            Console.Read();
        }

        static public void TEST()
        {
            string directory = @"E:\Mus";
            AudioFilesList testAu = new AudioFilesList(directory);
            var Au1 = testAu.Tracks[0];
            Console.WriteLine("{0} {1} {2} {3} {4} {5} {6}", 
                Au1.Album, Au1.Artist, Au1.Title, Au1.Genre, Au1.Year, Au1.Duration, Au1.TrackNumber);
        }

        private static void InsertTrack
            (string album, string artist, string title, string genre, string year, string duration, string trackNumber)
        {
            // название процедуры
            string sqlExpression = "sp_InsertTrack";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = CommandType.StoredProcedure;

                // параметр для ввода album
                SqlParameter albumParam = new SqlParameter
                {
                    ParameterName = "@album",
                    Value = album
                };
                command.Parameters.Add(albumParam);

                // параметр для ввода artist
                SqlParameter artistParam = new SqlParameter
                {
                    ParameterName = "@artist",
                    Value = artist
                };
                command.Parameters.Add(artistParam);

                // параметр для ввода title
                SqlParameter titleParam = new SqlParameter
                {
                    ParameterName = "@title",
                    Value = title
                };
                command.Parameters.Add(titleParam);

                // параметр для ввода genre
                SqlParameter genreParam = new SqlParameter
                {
                    ParameterName = "@genre",
                    Value = genre
                };
                command.Parameters.Add(genreParam);

                // параметр для ввода year
                SqlParameter yearParam = new SqlParameter
                {
                    ParameterName = "@year",
                    Value = year
                };
                command.Parameters.Add(yearParam);

                // параметр для ввода duration
                SqlParameter durationParam = new SqlParameter
                {
                    ParameterName = "@duration",
                    Value = duration
                };
                command.Parameters.Add(durationParam);

                // параметр для ввода trackNumber
                SqlParameter trackNumberParam = new SqlParameter
                {
                    ParameterName = "@trackNumber",
                    Value = Convert.ToInt32(trackNumber)
                };
                command.Parameters.Add(trackNumberParam);

                command.ExecuteNonQuery();
            }
        }

    }
}
